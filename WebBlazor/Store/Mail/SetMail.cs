using WebBlazor.Models.Address;
using WebBlazor.Models.DataModel;
using WebBlazor.Models.Mail;

namespace WebBlazor.Store.Mail
{
    public record SeTMail(DataTableResponse<MailDto> mailList);
}
