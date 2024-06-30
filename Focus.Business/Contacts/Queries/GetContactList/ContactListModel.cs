using System;
using System.Collections.Generic;
using System.Text;

namespace Focus.Business.Contacts.Queries.GetContactList
{
    public class ContactListModel
    {
        public IList<ContactLookUpModel> Contacts { get; set; }
    }
}
