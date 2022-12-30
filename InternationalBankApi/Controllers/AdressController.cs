using BusinessLayer.Abstract;
using BusinessLayer.Dto.Adress;
using BusinessLayer.Dto.District;
using CoreLayer.DataAccess.Concrete.DataRequest;
using EntityLayer.Models.EntityFremework;
using InternationalBankApi.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InternationalBankApi.Controllers
{
    public class AdressController : BaseController
    {
        private readonly IAdressWorkFlow _adressWorkFlow;

        public AdressController(IAdressWorkFlow adressWorkFlow)
        {
            this._adressWorkFlow = adressWorkFlow;
        }

        [HttpPost, Route("CustomerAdressList"), Authorize(Roles = "Customer")]
        public IActionResult CustomerAdressList([FromBody] DataTableRequest request)
        {
            var result = this._adressWorkFlow.CustomerAdressList(request);
            return Ok(result);
        }

        [HttpPost, Route("GetAdressDetail"), Authorize(Roles = "Customer")]
        public IActionResult GetAdressDetail([FromBody] DataRequest request)
        {
            var result = this._adressWorkFlow.GetAdressDetail(request);
            return Ok(result);
        }

        [HttpGet, Route("GetCountryDropDownList")]
        public IActionResult GetCountryDropDownList()
        {
            var result = this._adressWorkFlow.GetCountryDropDownList(true);
            return Ok(result);
        }

        [HttpGet, Route("GetCityDropDownList")]
        public IActionResult GetCityDropDownList(int CountryId)
        {
            var result = this._adressWorkFlow.GetCityDropDownList(true,CountryId);
            return Ok(result);
        }

        [HttpGet, Route("GetDistrictDropDownList")]
        public IActionResult GetDistrictDropDownList(int CityId)
        {
            var result = this._adressWorkFlow.GetDistrictDropDownList(true,CityId);
            return Ok(result);
        }

        [HttpPost, Route("UpdateAdress")]
        public IActionResult UpdateAdress([FromBody]AdressUpdateDto adress)
        {
            var result = this._adressWorkFlow.UpdateAdress(adress);
            return Ok(result);
        }

        [HttpPost, Route("DeleteAdress")]
        public IActionResult DeleteAdress([FromBody] AdressUpdateDto adress)
        {
            var result = this._adressWorkFlow.DeleteAdress(adress);
            return Ok(result);
        }

        [HttpPost, Route("AddAdress")]
        public IActionResult AddAdress([FromBody] AdressCreateDto adress)
        {
            var result = this._adressWorkFlow.AddAdress(adress);
            return Ok(result);
        }
    }
}
