using Fluxor;
using WebBlazor.Models.DataModel;
using WebBlazor.Models.Phone;

namespace WebBlazor.Store.Phone
{
    public record PhoneState
    {
        public DataTableResponse<PhoneNumberDto> PhoneList { get; set; }

        public class PhoneFeatureState : Feature<PhoneState>
        {
            public override string GetName()=>nameof(PhoneState);

            protected override PhoneState GetInitialState()
            {
                return new PhoneState
                {
                    PhoneList = null
                };
            }
        }
    }
}
