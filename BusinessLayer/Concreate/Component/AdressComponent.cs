using BusinessLayer.Dto.Adress;
using CoreLayer.BusinessLayer.Concreate.Component;
using CoreLayer.DataAccess.Concrete.DataRequest;

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

namespace BusinessLayer.Concreate.Component
{
    public class AdressComponent : ComponentBase
    {
        #region Variables
        protected readonly IUnitOfWork _unow;
        #endregion
        #region Constructor
        public AdressComponent(IUnitOfWork unow)
        {
            this._unow = unow;
        }
        #endregion

        #region protected method
        #region Async metod
        protected async Task<Result<DataResponse<Adress>>> GetAdressDetailComponentAsync(DataRequest request)
        {
            Result<DataResponse<Adress>> result = new Result<DataResponse<Adress>>();
            result.ResultObje = new DataResponse<Adress>();

            var query = this._unow.AdressRespository.Get(x => true);
            query = query.Include(x => x.District).Include(x => x.City).Include(x => x.Country);

            if (request.Filter.Any(x => x.Name == "Id") && request.Filter.FirstOrDefault(x => x.Name == "Id").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "Id").Value;
                int Id;
                int.TryParse(value, out Id);
                query = query.Where(x => x.Id == Id);
            }

            var resultData = await query.FirstOrDefaultAsync();

            if (!object.Equals(resultData, null))
            {
                result.ResultObje.Data = resultData;
                result.SetTrue();
            }
            return result;
        }

        protected async Task<Result<List<Country>>> GetCountryListComponentAsync(bool? IsActive)
        {
            Result<List<Country>> result = new Result<List<Country>>();

            var query = this._unow.CountryRepository.Get(x => true);
            if (IsActive.HasValue)
                query = query.Where(x => x.IsActive == IsActive.Value);

            var countryList = await query.OrderBy(x => x.CountryCode).ToListAsync();

            if (countryList.Count() > 0)
            {
                result.ResultObje = countryList;
                result.SetTrue();
            }
            return result;
        }

        protected async Task<Result<List<City>>> GetCityListComponentAsync(bool? IsActive, int CountryId = 0)
        {
            Result<List<City>> result = new Result<List<City>>();

            var query = this._unow.CityRepository.Get(x => true);

            if (IsActive.HasValue)
                query = query.Where(x => x.IsActive == IsActive.Value);

            if (CountryId > 0)
                query = query.Where(x => x.CountryId == CountryId);

            var cityList = await query.OrderBy(x => x.CityNumber).ToListAsync();
            if (cityList.Count() > 0)
            {
                result.ResultObje = cityList;
                result.SetTrue();
            }
            return result;
        }

        protected async Task<Result<List<District>>> GetDistrictListComponentAsync(bool? IsActive, int CityId = 0)
        {
            Result<List<District>> result = new Result<List<District>>();
            var query = this._unow.DistrictRepository.Get(x => true);

            if (IsActive.HasValue)
                query = query.Where(x => x.IsActive == IsActive.Value);

            if (CityId > 0)
                query = query.Where(x => x.CityId == CityId);

            var cityList = await query.OrderBy(x => x.DistrictName).ToListAsync();

            if (cityList.Count() > 0)
            {
                result.ResultObje = cityList;
                result.SetTrue();
            }
            return result;
        }

        protected async Task<Result> UpdateAdressComponentAsync(AdressUpdateDto adress)
        {
            Result result = new Result();
            var adressEntity = await this._unow.AdressRespository.Get(x => x.Id == adress.Id).FirstOrDefaultAsync();

            if (adressEntity != null)
            {
                this.AdressUpdateMapping(adressEntity, adress);
                var update = this._unow.AdressRespository.Update(adressEntity);
                if (update.ResultStatus && await this._unow.SaveChangesAsync())
                    result.SetTrue();
            }

            return result;
        }

        protected async Task<Result<DataTableResponse<Adress>>> CustomerAdressListComponentAsync(DataTableRequest request)
        {
            Result<DataTableResponse<Adress>> result = new Result<DataTableResponse<Adress>>();
            result.ResultObje = new DataTableResponse<Adress>();

            var query = this._unow.AdressRespository.Get(x => true);
            query = query.Include(x => x.District).Include(x => x.City);

            query = SetFilter(request.Filter, query);
            query = SetOrder(request.Order, query);

            int count = await query.CountAsync();

            if (count > 0)
            {
                var data = await query.Skip(request.Skip).Take(request.Take).AsQueryable().ToListAsync();

                result.ResultObje.Data = data;
                result.ResultObje.TotalCount = count;
                result.SetTrue();
            }

            return result;
        }

        protected async Task<Result> DeleteAdressComponentAsync(AdressUpdateDto adress)
        {
            Result result = new Result();
            var adressEntity = await this._unow.AdressRespository.Get(x => x.Id == adress.Id).FirstOrDefaultAsync();

            if (adressEntity != null)
            {
                adressEntity.IsActive = false;
                var update = this._unow.AdressRespository.Update(adressEntity);
                if (update.ResultStatus && await this._unow.SaveChangesAsync())
                    result.SetTrue();
            }

            return result;
        }

        protected async Task<Result<Adress>> AddAdressComponentAsync(Adress adress)
        {
            Result<Adress> result = new Result<Adress>();
            var addResult = await this._unow.AdressRespository.AddAsync(adress);
            if (addResult.ResultStatus && await this._unow.SaveChangesAsync())
            {
                result.ResultObje = adress;
                result.SetTrue();
            }

            return result;
        }
        #endregion
        protected Result<DataResponse<Adress>> GetAdressDetailComponent(DataRequest request)
        {
            Result<DataResponse<Adress>> result = new Result<DataResponse<Adress>>();
            result.ResultObje = new DataResponse<Adress>();

            var query = this._unow.AdressRespository.Get(x => true);
            query = query.Include(x => x.District).Include(x => x.City).Include(x => x.Country);

            if (request.Filter.Any(x => x.Name == "Id") && request.Filter.FirstOrDefault(x => x.Name == "Id").Value != null)
            {
                string value = request.Filter.FirstOrDefault(x => x.Name == "Id").Value;
                int Id;
                int.TryParse(value, out Id);
                query = query.Where(x => x.Id == Id);
            }

            var resultData = query.FirstOrDefault();

            if (!object.Equals(resultData, null))
            {
                result.ResultObje.Data = resultData;
                result.SetTrue();
            }
            return result;
        }

        protected Result<List<Country>> GetCountryListComponent(bool? IsActive)
        {
            Result<List<Country>> result = new Result<List<Country>>();

            var query = this._unow.CountryRepository.Get(x => true);
            if (IsActive.HasValue)
                query = query.Where(x => x.IsActive == IsActive.Value);

            var countryList = query.OrderBy(x => x.CountryCode).ToList();

            if (countryList.Count() > 0)
            {
                result.ResultObje = countryList;
                result.SetTrue();
            }
            return result;
        }

        protected Result<DataTableResponse<Adress>> CustomerAdressListComponent(DataTableRequest request)
        {
            Result<DataTableResponse<Adress>> result = new Result<DataTableResponse<Adress>>();
            result.ResultObje = new DataTableResponse<Adress>();

            var query = this._unow.AdressRespository.Get(x => true);
            query = query.Include(x => x.District).Include(x => x.City);

            query = SetFilter(request.Filter, query);
            query = SetOrder(request.Order, query);

            int count = query.Count();

            if (count > 0)
            {
                var data = query.Skip(request.Skip).Take(request.Take).AsQueryable().ToList();

                result.ResultObje.Data = data;
                result.ResultObje.TotalCount = count;
                result.SetTrue();
            }

            return result;
        }

        protected Result<List<City>> GetCityListComponent(bool? IsActive, int CountryId = 0)
        {
            Result<List<City>> result = new Result<List<City>>();

            var query = this._unow.CityRepository.Get(x => true);

            if (IsActive.HasValue)
                query = query.Where(x => x.IsActive == IsActive.Value);

            if (CountryId > 0)
                query = query.Where(x => x.CountryId == CountryId);

            var cityList = query.OrderBy(x => x.CityNumber).ToList();
            if (cityList.Count() > 0)
            {
                result.ResultObje = cityList;
                result.SetTrue();
            }
            return result;
        }
        protected Result<List<District>> GetDistrictListComponent(bool? IsActive, int CityId = 0)
        {
            Result<List<District>> result = new Result<List<District>>();
            var query = this._unow.DistrictRepository.Get(x => true);

            if (IsActive.HasValue)
                query = query.Where(x => x.IsActive == IsActive.Value);

            if (CityId > 0)
                query = query.Where(x => x.CityId == CityId);

            var cityList = query.OrderBy(x => x.DistrictName).ToList();

            if (cityList.Count() > 0)
            {
                result.ResultObje = cityList;
                result.SetTrue();
            }
            return result;
        }

        protected Result UpdateAdressComponent(AdressUpdateDto adress)
        {
            Result result = new Result();
            var adressEntity = this._unow.AdressRespository.Get(x => x.Id == adress.Id).FirstOrDefault();

            if (adressEntity != null)
            {
                this.AdressUpdateMapping(adressEntity, adress);
                var update = this._unow.AdressRespository.Update(adressEntity);
                if (update.ResultStatus && this._unow.SaveChanges())
                    result.SetTrue();
            }

            return result;
        }
        protected Result DeleteAdressComponent(AdressUpdateDto adress)
        {
            Result result = new Result();
            var adressEntity = this._unow.AdressRespository.Get(x => x.Id == adress.Id).FirstOrDefault();

            if (adressEntity != null)
            {
                adressEntity.IsActive = false;
                var update = this._unow.AdressRespository.Update(adressEntity);
                if (update.ResultStatus && this._unow.SaveChanges())
                    result.SetTrue();
            }

            return result;
        }
        protected Result<Adress> AddAdressComponent(Adress adress)
        {
            Result<Adress> result = new Result<Adress>();
            var addResult = this._unow.AdressRespository.Add(adress);
            if (addResult.ResultStatus && this._unow.SaveChanges())
            {
                result.ResultObje = adress;
                result.SetTrue();
            }

            return result;
        }
        #endregion
        private IQueryable<Adress> SetOrder(OrderModel order, IQueryable<Adress> query)
        {
            switch (order.Column)
            {
                case "CityId":
                    if (order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.CityId);
                    else
                        query = query.OrderBy(x => x.CityId);
                    break;
                case "DistrictId":
                    if (order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.DistrictId);
                    else
                        query = query.OrderBy(x => x.DistrictId);
                    break;
                case "IsFavorite":
                    if (order.Short.ToUpper() == "DESC")
                        query = query.OrderByDescending(x => x.IsFavorite);
                    else
                        query = query.OrderBy(x => x.IsFavorite);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreateDate);
                    break;
            }
            return query;
        }
        private IQueryable<Adress> SetFilter(List<NameValuePair> valuePairs, IQueryable<Adress> query)
        {
            if (valuePairs.Any(x => x.Name == "Id") && valuePairs.FirstOrDefault(x => x.Name == "Id").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "Id").Value;
                int Id;
                int.TryParse(value, out Id);
                query = query.Where(x => x.Id == Id);
            }
            if (valuePairs.Any(x => x.Name == "CustomerId") && valuePairs.FirstOrDefault(x => x.Name == "CustomerId").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "CustomerId").Value;
                int CustomerId;
                int.TryParse(value, out CustomerId);
                query = query.Where(x => x.CustomerId == CustomerId);
            }
            if (valuePairs.Any(x => x.Name == "AdressName") && valuePairs.FirstOrDefault(x => x.Name == "AdressName").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "AdressName").Value;
                query = query.Where(x => value.Contains(x.AdressName));
            }
            if (valuePairs.Any(x => x.Name == "AdressDetail") && valuePairs.FirstOrDefault(x => x.Name == "AdressDetail").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "AdressDetail").Value;
                query = query.Where(x => value.Contains(x.AdressDetail));
            }
            if (valuePairs.Any(x => x.Name == "CityId") && valuePairs.FirstOrDefault(x => x.Name == "CityId").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "CityId").Value;
                int CityId;
                int.TryParse(value, out CityId);
                query = query.Where(x => x.CityId == CityId);
            }
            if (valuePairs.Any(x => x.Name == "DistrictId") && valuePairs.FirstOrDefault(x => x.Name == "DistrictId").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "DistrictId").Value;
                int DistrictId;
                int.TryParse(value, out DistrictId);
                query = query.Where(x => x.DistrictId == DistrictId);
            }
            if (valuePairs.Any(x => x.Name == "CountryId") && valuePairs.FirstOrDefault(x => x.Name == "CountryId").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "CountryId").Value;
                int CountryId;
                int.TryParse(value, out CountryId);
                query = query.Where(x => x.CountryId == CountryId);
            }
            if (valuePairs.Any(x => x.Name == "IsActive") && valuePairs.FirstOrDefault(x => x.Name == "IsActive").Value != null)
            {
                string value = valuePairs.FirstOrDefault(x => x.Name == "IsActive").Value;
                bool IsActive;
                bool.TryParse(value, out IsActive);
                query = query.Where(x => x.IsActive == IsActive);
            }

            return query;
        }
        private void AdressUpdateMapping(Adress adress, AdressUpdateDto updateDto)
        {
            if (!string.IsNullOrEmpty(updateDto.AdressDetail))
                adress.AdressDetail = updateDto.AdressDetail;
            adress.AdressName = updateDto.AdressName == string.Empty ? null : updateDto.AdressName;
            if (updateDto.IsFavorite.HasValue)
                adress.IsFavorite = updateDto.IsFavorite.Value;
            if (updateDto.DistrictId.HasValue)
                adress.DistrictId = updateDto.DistrictId.Value;
            if (updateDto.CityId.HasValue)
                adress.CityId = updateDto.CityId.Value;
            if (updateDto.CountryId.HasValue)
                adress.CountryId = updateDto.CountryId.Value;
            adress.DomicileStartDate = updateDto.DomicileStartDate;
            if (updateDto.IsActive.HasValue)
                adress.IsActive = updateDto.IsActive.Value;
        }
    }
}
