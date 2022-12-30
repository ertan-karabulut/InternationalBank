using Fluxor;
using WebBlazor.Models.Address;
using WebBlazor.Models.DataModel;
using WebBlazor.Models.Mail;
using WebBlazor.Models.Phone;

namespace WebBlazor.Store.Mail
{
    public record MailState
    {
        public DataTableResponse<MailDto> MailList { get; set; }

        public class MailFeatureState : Feature<MailState>
        {
            public override string GetName() => nameof(MailState);

            protected override MailState GetInitialState()
            {
                return new MailState
                {
                    MailList = null
                };
            }
        }
    }
}
