using BusinessLayer.Dto;
using BusinessLayer.Dto.Adress;
using CoreLayer.DataAccess.Concrete.DataRequest;
using CoreLayer.Utilities.Result.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAdressWorkFlow 
    {
        Result<DataTableResponse<AdressDto>> CustomerAdressList(DataTableRequest request);
        Result<DataResponse<AdressDetailDto>> GetAdressDetail(DataRequest request);
        Result<List<SelectListItemDto>> GetCountryDropDownList(bool? IsActive);
        Result<List<SelectListItemDto>> GetCityDropDownList(bool? IsActive, int CountryId = 0);
        Result<List<SelectListItemDto>> GetDistrictDropDownList(bool? IsActive, int CityId = 0);
        Result UpdateAdress(AdressUpdateDto adress);
        Result DeleteAdress(AdressUpdateDto adress);
        Result<AdressDto> AddAdress(AdressCreateDto adress);
        #region Async metod
        Task<Result<DataTableResponse<AdressDto>>> CustomerAdressListAsync(DataTableRequest request);
        Task<Result<DataResponse<AdressDetailDto>>> GetAdressDetailAsync(DataRequest request);
        Task<Result<List<SelectListItemDto>>> GetCountryDropDownListAsync(bool? IsActive);
        Task<Result<List<SelectListItemDto>>> GetCityDropDownListAsync(bool ?IsActive,int CountryId = 0);
        Task<Result<List<SelectListItemDto>>> GetDistrictDropDownListAsync(bool ?IsActive,int CityId = 0);
        Task<Result> UpdateAdressAsync(AdressUpdateDto adress);
        Task<Result> DeleteAdressAsync(AdressUpdateDto adress);
        Task<Result<AdressDto>> AddAdressAsync(AdressCreateDto adress);
        #endregion
    }
}
