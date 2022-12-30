using WebBlazor.Models.DataModel;
using WebBlazor.Models.Phone;

namespace WebBlazor.Store.Phone
{
    public record SetPhone(DataTableResponse<PhoneNumberDto> phoneList);
}
