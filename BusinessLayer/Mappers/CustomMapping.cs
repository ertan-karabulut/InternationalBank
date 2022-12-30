using AutoMapper;
using BusinessLayer.Dto;
using BusinessLayer.Dto.Account;
using BusinessLayer.Dto.Adress;
using BusinessLayer.Dto.Branch;
using BusinessLayer.Dto.Card;
using BusinessLayer.Dto.District;
using BusinessLayer.Dto.Mail;
using BusinessLayer.Dto.PhpneNumber;
using CoreLayer.Utilities.Helpers;
using EntityLayer.Models.EntityFremework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mappers
{
    public class CustomMapping : Profile
    {
        public CustomMapping()
        {
            HelperWorkFlow helper = new HelperWorkFlow();
            #region Account Map
            CreateMap<AccountDto, Account>().ReverseMap();
            CreateMap<AccountCreatDto, Account>().ReverseMap();
            CreateMap<AccountUpdateDto, Account>().ReverseMap();
            CreateMap<Account, AccountDetailDto>().ForMember(x=>x.Iban, x=>x.MapFrom(y=>helper.IbanFormat(y.Iban))).ForMember(x=>x.CustomerNumber,x=>x.MapFrom(y=>y.Customer.CustomerNumber)).ForMember(x=>x.CustomerFullName,x=>x.MapFrom(y=> $"{y.Customer.Name.ToUpper()} {y.Customer.Surname.ToUpper()}")).ForMember(x=>x.Account , x=>x.MapFrom(y=>$"{y.Branch.BranchNumber} - {y.AccountNumber}")).ForMember(x=>x.AccountOpenDate,x=>x.MapFrom(y=>y.CreateDate)).ForMember(x=>x.AccountType,x=>x.MapFrom(y=>$"{y.Type.TypeName} Hesap")).ForMember(x=>x.CurrencyCode,x=>x.MapFrom(y=>y.CurrencyUnit.ShortName)).ForMember(x=>x.Balance, x=>x.MapFrom(y=>y.AccountBalances.Count()>0? y.AccountBalances.FirstOrDefault().Balance.ToString("N2"):"0,00")).ForMember(x=>x.AvailableBalance, x=> x.MapFrom(y=> ((y.AdditionalAccounts.Count() > 0 ? y.AdditionalAccounts.FirstOrDefault().Balance:0) + (y.AccountBalances.Count()>0?y.AccountBalances.FirstOrDefault().Balance:0)).ToString("N2"))).ForMember(x=>x.BalanceDate, x=>x.MapFrom(y=>y.CreateDate)).ForMember(x=>x.BranchName, x=>x.MapFrom(y=>y.Branch.BranchName));
            CreateMap<Account, MyAccountDto>().ForMember(x => x.Account, x => x.MapFrom(y => $"{y.Branch.BranchNumber} - {y.AccountNumber} {y.Branch.BranchName}")).ForMember(x => x.Iban, x => x.MapFrom(y => helper.IbanFormat(y.Iban))).ForMember(x => x.AccountType, x => x.MapFrom(y => $"{y.Type.TypeName} {y.CurrencyUnit.ShortName}")).ForMember(x=>x.BalanceStr, x=>x.MapFrom(y=> (y.AccountBalances.Count()>0? y.AccountBalances.FirstOrDefault().Balance:0).ToString("N2") + $" {y.CurrencyUnit.ShortName}")).ForMember(x=>x.ShortName, x=>x.MapFrom(y=>y.CurrencyUnit.ShortName)).ForMember(x=>x.AvailableBalance, x=>x.MapFrom(y=> (y.AccountBalances.Count()>0 ? y.AccountBalances.FirstOrDefault().Balance:0) + (y.AdditionalAccounts.Count()>0 ? y.AdditionalAccounts.FirstOrDefault().Balance:0))).ForMember(x => x.AvailableBalanceStr, x => x.MapFrom(y => ((y.AccountBalances.Count()>0? y.AccountBalances.FirstOrDefault().Balance:0) + (y.AdditionalAccounts.Count()>0? y.AdditionalAccounts.FirstOrDefault().Balance:0)).ToString("N2")+$" {y.CurrencyUnit.ShortName}"));
            CreateMap<AccountBalanceHistory, AccountHistoryDto>().ReverseMap();
            #endregion

            #region Address Map
            CreateMap<Adress, AdressDto>().ForMember(x=> x.CityName, x=>x.MapFrom(y=> y.City.CityName)).ForMember(x => x.DistrictName, x => x.MapFrom(y => y.District.DistrictName)).ForMember(x => x.CountryName, x => x.MapFrom(y => y.Country.CountryName)).ReverseMap();
            CreateMap<Adress, AdressUpdateDto>().ReverseMap();
            CreateMap<Adress, AdressCreateDto>().ReverseMap();
            #endregion

            #region Branch Map
            CreateMap<Branch, BranchDto>().ForMember(x=> x.CityName, x=>x.MapFrom(y=>y.City.CityName)).ForMember(x => x.DistrictName, x => x.MapFrom(y => y.District.DistrictName)).ReverseMap();
            #endregion

            #region EMail Map
            CreateMap<EMail, MailDto>().ForMember(x=>x.EMail, x=>x.MapFrom(y=>y.EMail1)).ReverseMap();
            CreateMap<EMail, MailCreateDto>().ForMember(x => x.EMail, x => x.MapFrom(y => y.EMail1)).ReverseMap();
            #endregion
            
            #region PhoneNumber Map
            CreateMap<PhoneNumber, PhoneNumberDto>().ForMember(x => x.PhoneNumber, x => x.MapFrom(y => y.PhoneNumber1)).ReverseMap();

            CreateMap<PhoneNumber, PhoneNumberCreateDto>().ForMember(x => x.PhoneNumber, x => x.MapFrom(y => y.PhoneNumber1)).ReverseMap();
            #endregion

            #region Card Map
            CreateMap<CreditCard, CreditCardDto>().ForMember(x=>x.CustomerId, x=>x.MapFrom(y=>y.CreditCardNavigation.CustomerId)).ForMember(x=>x.CreateDate, x=>x.MapFrom(y=>y.CreditCardNavigation.CreateDate)).ForMember(x=>x.CardNumber,x=>x.MapFrom(y=>y.CreditCardNavigation.CardNumber)).ForMember(x=>x.IsActive , x=>x.MapFrom(y=>y.CreditCardNavigation.IsActive)).ForMember(x=>x.Id,x=>x.MapFrom(y=>y.CreditCardNavigation.Id)).ForMember(x=>x.CreditCardLimit, x=>x.MapFrom(y=>y.CreditCardLimit.ToString("N"))).ReverseMap();
            CreateMap<AtmCard, AtmCardDto>().ForMember(x => x.Id, x => x.MapFrom(y => y.AtmCardNavigation.Id)).ForMember(x => x.CreateDate, x => x.MapFrom(y => y.AtmCardNavigation.CreateDate)).ForMember(x => x.CustomerId, x => x.MapFrom(y => y.AtmCardNavigation.CustomerId)).ForMember(x => x.CardNumber, x => x.MapFrom(y => y.AtmCardNavigation.CardNumber)).ForMember(x => x.IsActive, x => x.MapFrom(y => y.AtmCardNavigation.IsActive)).ReverseMap();
            #endregion
            CreateMap<District, DistrictCreateDto>().ReverseMap();
            CreateMap<District, DistrictDto>().ReverseMap();
        }
    }
}
