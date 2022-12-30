using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Concreate.Component;
using BusinessLayer.Dto;
using BusinessLayer.Dto.Adress;
using BusinessLayer.Validation;
using BusinessLayer.Validation.AdressValidation;
using CoreLayer.DataAccess.Concrete.DataRequest;
using CoreLayer.Utilities.Aspect;
using CoreLayer.Utilities.Cache.Concreate;
using CoreLayer.Utilities.Result.Concreate;
using DataAccessLayer.Abstract;
using EntityLayer.Models.EntityFremework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concreate.WorkFlow
{
    public class AdressWorkFlow : AdressComponent, IAdressWorkFlow
    {
        #region Variables
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public AdressWorkFlow(IUnitOfWork unow, IMapper mapper) : base(unow)
        {
            this._mapper = mapper;
        }
        #endregion

        #region Async metod
        [ValidatorAspect(typeof(DataValidation))]
        public async Task<Result<DataResponse<AdressDetailDto>>> GetAdressDetailAsync(DataRequest request)
        {
            Result<DataResponse<AdressDetailDto>> result =
                new Result<DataResponse<AdressDetailDto>>();

            if (!request.Filter.Any(x => x.Name == "Id"))
                return result;

            var adress = await base.GetAdressDetailComponentAsync(request);

            if (adress.ResultStatus)
            {
                result.ResultObje = new DataResponse<AdressDetailDto>();
                result.ResultObje.Data = new AdressDetailDto();
                result.ResultObje.Data.Adress = this._mapper.Map<AdressDto>(adress.ResultObje.Data);


                var countryList = await this.GetCountryDropDownListAsync(true);
                if (countryList.ResultStatus)
                {
                    countryList.ResultObje.ForEach(x => x.Selected = int.Parse(x.Value) == result.ResultObje.Data.Adress.CountryId);
                    result.ResultObje.Data.CountrySelectList = countryList.ResultObje.OrderByDescending(x => x.Selected).ToList();
                }

                var cityList = await this.GetCityDropDownListAsync(true, result.ResultObje.Data.Adress.CountryId);

                if (cityList.ResultStatus)
                {
                    cityList.ResultObje.ForEach(x => x.Selected = int.Parse(x.Value) == result.ResultObje.Data.Adress.CityId);
                    result.ResultObje.Data.CitySelectList = cityList.ResultObje.OrderByDescending(x => x.Selected).ToList();
                }

                var districtList = await this.GetDistrictDropDownListAsync(true, result.ResultObje.Data.Adress.CityId);

                if (districtList.ResultStatus)
                {
                    districtList.ResultObje.ForEach(x => x.Selected = int.Parse(x.Value) == result.ResultObje.Data.Adress.DistrictId);
                    result.ResultObje.Data.DistrictSelectList = districtList.ResultObje.OrderByDescending(x => x.Selected).ToList();
                }

                result.SetTrue();
            }

            return result;
        }

        [CacheAspect]
        public async Task<Result<List<SelectListItemDto>>> GetCountryDropDownListAsync(bool? IsActive)
        {
            Result<List<SelectListItemDto>> result = new Result<List<SelectListItemDto>>();

            var cityList = await base.GetCountryListComponentAsync(IsActive);
            if (cityList.ResultStatus)
            {
                result.ResultObje = cityList.ResultObje.Select(x => new SelectListItemDto { Text = x.CountryName, Value = x.Id.ToString(), Selected = x.CountryCode == "TR" }).ToList();
                result.SetTrue();
            }
            return result;
        }

        [CacheAspect]
        public async Task<Result<List<SelectListItemDto>>> GetCityDropDownListAsync(bool? IsActive, int CountryId)
        {
            Result<List<SelectListItemDto>> result = new Result<List<SelectListItemDto>>();

            var cityList = await base.GetCityListComponentAsync(CountryId: CountryId, IsActive: IsActive);
            if (cityList.ResultStatus)
            {
                result.ResultObje = cityList.ResultObje.Select(x => new SelectListItemDto { Text = x.CityName, Value = x.Id.ToString() }).ToList();
                result.SetTrue();
            }
            return result;
        }

        [CacheAspect]
        public async Task<Result<List<SelectListItemDto>>> GetDistrictDropDownListAsync(bool? IsActive, int CityId)
        {
            Result<List<SelectListItemDto>> result = new Result<List<SelectListItemDto>>();

            var cityList = await base.GetDistrictListComponentAsync(CityId: CityId, IsActive: IsActive);
            if (cityList.ResultStatus)
            {
                result.ResultObje = cityList.ResultObje.Select(x => new SelectListItemDto { Text = x.DistrictName, Value = x.Id.ToString() }).ToList();
                result.SetTrue();
            }
            return result;
        }

        [ValidatorAspect(typeof(UpdateAdressValidation))]
        public async Task<Result> UpdateAdressAsync(AdressUpdateDto adress)
        {
            base._unow.BeginTransaction();
            if (adress.IsFavorite.HasValue)
            {
                var favoriteAdress = await base._unow.AdressRespository.Get(x => x.CustomerId == adress.CustomerId && x.IsFavorite == adress.IsFavorite.Value).FirstOrDefaultAsync();
                if (favoriteAdress != null && favoriteAdress.Id != adress.Id)
                {
                    favoriteAdress.IsFavorite = false;
                    base._unow.AdressRespository.Update(favoriteAdress);
                }
            }
            var adressUpdate = await base.UpdateAdressComponentAsync(adress);
            if (adressUpdate.ResultStatus)
                await base._unow.CommitAsync();
            else
                await base._unow.RollbackAsync();
            return adressUpdate;
        }

        [ValidatorAspect(typeof(DataTableValidation))]
        public async Task<Result<DataTableResponse<AdressDto>>> CustomerAdressListAsync(DataTableRequest request)
        {
            Result<DataTableResponse<AdressDto>> result = new Result<DataTableResponse<AdressDto>>();

            int customerId = base.helperWorkFlow.GetUserId();

            if (!request.Filter.Any(x => x.Name == "CustomerId"))
                request.Filter.Add(new NameValuePair { Name = "CustomerId", Value = customerId.ToString() });

            if (!request.Filter.Any(x => x.Name == "IsActive"))
                request.Filter.Add(new NameValuePair { Name = "IsActive", Value = "True" });

            if (request.Order.Column != "IsFavorite")
            {
                request.Order.Column = "IsFavorite";
                request.Order.Short = "desc";
            }

            var adress = await base.CustomerAdressListComponentAsync(request);
            if (adress.ResultStatus)
            {
                result.ResultObje = new DataTableResponse<AdressDto>();

                result.ResultObje.Data = this._mapper.Map<List<AdressDto>>(adress.ResultObje.Data);
                result.ResultObje.TotalCount = adress.ResultObje.TotalCount;
                result.SetTrue();
            }
            return result;
        }

        [ValidatorAspect(typeof(UpdateAdressValidation))]
        public async Task<Result> DeleteAdressAsync(AdressUpdateDto adress)
        {
            Result result = new Result();
            bool isAdress = base._unow.AdressRespository.Get(x => true).Any(x => x.CustomerId == adress.CustomerId && x.IsActive);

            if (isAdress)
            {
                result = await base.DeleteAdressComponentAsync(adress);
            }

            return result;
        }

        [ValidatorAspect(typeof(CreateAdressValidation))]
        public async Task<Result<AdressDto>> AddAdressAsync(AdressCreateDto adress)
        {
            Result<AdressDto> result = new Result<AdressDto>();
            adress.CustomerId = base.helperWorkFlow.GetUserId();

            if (adress.IsFavorite)
            {
                var favoriteAdress = await base._unow.AdressRespository.Get(x => x.CustomerId == adress.CustomerId && x.IsActive && x.IsFavorite).FirstOrDefaultAsync();
                if (favoriteAdress != null)
                {
                    favoriteAdress.IsFavorite = false;
                    if (!(base._unow.AdressRespository.Update(favoriteAdress).ResultStatus && await base._unow.SaveChangesAsync()))
                    {
                        result.SetFalse();
                    }
                }
            }

            var adressEntity = this._mapper.Map<Adress>(adress);
            var adressAdd = await base.AddAdressComponentAsync(adressEntity);
            if (adressAdd.ResultStatus)
            {
                result.SetTrue();
                result.ResultObje = this._mapper.Map<AdressDto>(adressAdd.ResultObje);
            }
            return result;
        }
        #endregion

        [ValidatorAspect(typeof(DataTableValidation))]
        public Result<DataTableResponse<AdressDto>> CustomerAdressList(DataTableRequest request)
        {
            Result<DataTableResponse<AdressDto>> result = new Result<DataTableResponse<AdressDto>>();

            int customerId = base.helperWorkFlow.GetUserId();

            if (!request.Filter.Any(x => x.Name == "CustomerId"))
                request.Filter.Add(new NameValuePair { Name = "CustomerId", Value = customerId.ToString() });

            if (!request.Filter.Any(x => x.Name == "IsActive"))
                request.Filter.Add(new NameValuePair { Name = "IsActive", Value = "True" });

            if (request.Order.Column != "IsFavorite")
            {
                request.Order.Column = "IsFavorite";
                request.Order.Short = "desc";
            }

            var adress = base.CustomerAdressListComponent(request);
            if (adress.ResultStatus)
            {
                result.ResultObje = new DataTableResponse<AdressDto>();

                result.ResultObje.Data = this._mapper.Map<List<AdressDto>>(adress.ResultObje.Data);
                result.ResultObje.TotalCount = adress.ResultObje.TotalCount;
                result.SetTrue();
            }
            return result;
        }
        [ValidatorAspect(typeof(DataValidation))]
        public Result<DataResponse<AdressDetailDto>> GetAdressDetail(DataRequest request)
        {
            Result<DataResponse<AdressDetailDto>> result =
                new Result<DataResponse<AdressDetailDto>>();

            if (!request.Filter.Any(x => x.Name == "Id"))
                return result;

            var adress = base.GetAdressDetailComponent(request);

            if (adress.ResultStatus)
            {
                result.ResultObje = new DataResponse<AdressDetailDto>();
                result.ResultObje.Data = new AdressDetailDto();
                result.ResultObje.Data.Adress = this._mapper.Map<AdressDto>(adress.ResultObje.Data);


                var countryList = this.GetCountryDropDownList(true);
                if (countryList.ResultStatus)
                {
                    countryList.ResultObje.ForEach(x => x.Selected = int.Parse(x.Value) == result.ResultObje.Data.Adress.CountryId);
                    result.ResultObje.Data.CountrySelectList = countryList.ResultObje.OrderByDescending(x => x.Selected).ToList();
                }

                var cityList = this.GetCityDropDownList(true, result.ResultObje.Data.Adress.CountryId);

                if (cityList.ResultStatus)
                {
                    cityList.ResultObje.ForEach(x => x.Selected = int.Parse(x.Value) == result.ResultObje.Data.Adress.CityId);
                    result.ResultObje.Data.CitySelectList = cityList.ResultObje.OrderByDescending(x => x.Selected).ToList();
                }

                var districtList = this.GetDistrictDropDownList(true, result.ResultObje.Data.Adress.CityId);

                if (districtList.ResultStatus)
                {
                    districtList.ResultObje.ForEach(x => x.Selected = int.Parse(x.Value) == result.ResultObje.Data.Adress.DistrictId);
                    result.ResultObje.Data.DistrictSelectList = districtList.ResultObje.OrderByDescending(x => x.Selected).ToList();
                }

                result.SetTrue();
            }

            return result;
        }
        [CacheAspect]
        public Result<List<SelectListItemDto>> GetCountryDropDownList(bool? IsActive)
        {
            Result<List<SelectListItemDto>> result = new Result<List<SelectListItemDto>>();

            var cityList = base.GetCountryListComponent(IsActive);
            if (cityList.ResultStatus)
            {
                result.ResultObje = cityList.ResultObje.Select(x => new SelectListItemDto { Text = x.CountryName, Value = x.Id.ToString(), Selected = x.CountryCode == "TR" }).ToList();
                result.SetTrue();
            }
            return result;
        }
        [CacheAspect]
        public Result<List<SelectListItemDto>> GetCityDropDownList(bool? IsActive, int CountryId = 0)
        {
            Result<List<SelectListItemDto>> result = new Result<List<SelectListItemDto>>();

            var cityList = base.GetCityListComponent(CountryId: CountryId, IsActive: IsActive);
            if (cityList.ResultStatus)
            {
                result.ResultObje = cityList.ResultObje.Select(x => new SelectListItemDto { Text = x.CityName, Value = x.Id.ToString() }).ToList();
                result.SetTrue();
            }
            return result;
        }
        [CacheAspect]
        public Result<List<SelectListItemDto>> GetDistrictDropDownList(bool? IsActive, int CityId = 0)
        {
            Result<List<SelectListItemDto>> result = new Result<List<SelectListItemDto>>();

            var cityList = base.GetDistrictListComponent(CityId: CityId, IsActive: IsActive);
            if (cityList.ResultStatus)
            {
                result.ResultObje = cityList.ResultObje.Select(x => new SelectListItemDto { Text = x.DistrictName, Value = x.Id.ToString() }).ToList();
                result.SetTrue();
            }
            return result;
        }
        [ValidatorAspect(typeof(UpdateAdressValidation))]
        public Result UpdateAdress(AdressUpdateDto adress)
        {
            base._unow.BeginTransaction();
            if (adress.IsFavorite.HasValue)
            {
                var favoriteAdress = base._unow.AdressRespository.Get(x => x.CustomerId == adress.CustomerId && x.IsFavorite == adress.IsFavorite.Value).FirstOrDefault();
                if (favoriteAdress != null && favoriteAdress.Id != adress.Id)
                {
                    favoriteAdress.IsFavorite = false;
                    base._unow.AdressRespository.Update(favoriteAdress);
                }
            }
            var adressUpdate = base.UpdateAdressComponent(adress);
            if (adressUpdate.ResultStatus)
                base._unow.Commit();
            else
                base._unow.Rollback();
            return adressUpdate;
        }
        [ValidatorAspect(typeof(UpdateAdressValidation))]
        public Result DeleteAdress(AdressUpdateDto adress)
        {
            Result result = new Result();
            bool isAdress = base._unow.AdressRespository.Get(x => true).Any(x => x.CustomerId == adress.CustomerId && x.IsActive);

            if (isAdress)
            {
                result = base.DeleteAdressComponent(adress);
            }

            return result;
        }
        [ValidatorAspect(typeof(CreateAdressValidation))]
        public Result<AdressDto> AddAdress(AdressCreateDto adress)
        {
            Result<AdressDto> result = new Result<AdressDto>();
            adress.CustomerId = base.helperWorkFlow.GetUserId();

            if (adress.IsFavorite)
            {
                var favoriteAdress = base._unow.AdressRespository.Get(x => x.CustomerId == adress.CustomerId && x.IsActive && x.IsFavorite).FirstOrDefault();
                if (favoriteAdress != null)
                {
                    favoriteAdress.IsFavorite = false;
                    if (!(base._unow.AdressRespository.Update(favoriteAdress).ResultStatus && base._unow.SaveChanges()))
                    {
                        result.SetFalse();
                    }
                }
            }

            var adressEntity = this._mapper.Map<Adress>(adress);
            var adressAdd = base.AddAdressComponent(adressEntity);
            if (adressAdd.ResultStatus)
            {
                result.SetTrue();
                result.ResultObje = this._mapper.Map<AdressDto>(adressAdd.ResultObje);
            }
            return result;
        }
    }
}
