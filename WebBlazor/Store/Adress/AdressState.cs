using Fluxor;
using System.Collections.Generic;
using WebBlazor.Models;
using WebBlazor.Models.Address;
using WebBlazor.Models.DataModel;

namespace WebBlazor.Store.Account
{
    public record AdressState
    {
        public List<AdressDto> AdressList { get; set; }
    }

    public class AdressFeatureState : Feature<AdressState>
    {
        public override string GetName() => nameof(AdressState);

        protected override AdressState GetInitialState()
        {
            return new AdressState
            {
                AdressList = null
            };
        }
    }
}
