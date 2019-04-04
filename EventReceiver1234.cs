using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace cobaincrementpastibisa.EventReceiver1234
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class EventReceiver1234 : SPItemEventReceiver
    {
        static int angkadeleted;        
        /// <summary>
        /// An item is being deleted.
        /// </summary>
        public override void ItemDeleting(SPItemEventProperties properties)
        {
            base.ItemDeleting(properties);
            SPListItem item = properties.ListItem;
            angkadeleted=Convert.ToInt32(item["ID_Counter"]);            
        }        
        
        /// <summary>
        /// An item was added.
        /// </summary>
        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            int angkaterbesar = 0;
            SPList coba = properties.Web.Lists["ListKaryawanBaru"];
            SPQuery q = new SPQuery();
            q.Query = @"<OrderBy>
                  <FieldRef Name='ID_Counter' Ascending='False' />
               </OrderBy>";
            q.RowLimit = 1;
            SPListItemCollection items = coba.GetItems(q);
            foreach (SPItem a in items)
            {
                angkaterbesar = Convert.ToInt32(a["ID_Counter"]) + 1;
            }

            bool allowUnsafeUpdate = properties.Web.AllowUnsafeUpdates;
            SPList objSPList = properties.Web.Lists["ListKaryawanBaru"];
            if (objSPList != null)
            {
                properties.Web.AllowUnsafeUpdates = true;
                var query = new SPQuery
                {
                    Query = @"<OrderBy>
                              <FieldRef Name='ID' Ascending='False' />
                           </OrderBy>"
                };
                query.RowLimit = 1;
                var a = objSPList.GetItems(query);
                foreach (SPListItem b in a)
                {
                    b["ID_Counter"] = angkaterbesar;
                    b.Update();
                }
            }
        }

        /// <summary>
        /// An item was deleted.
        /// </summary>
        public override void ItemDeleted(SPItemEventProperties properties)
        {
            base.ItemDeleted(properties);
            bool allowUnsafeUpdate = properties.Web.AllowUnsafeUpdates;
            SPList objSPList = properties.Web.Lists["ListKaryawanBaru"];
            if (objSPList != null)
            {
                properties.Web.AllowUnsafeUpdates = true;
                var query = new SPQuery
                {
                    Query = @"<Where>
                  <Gt>
                     <FieldRef Name='ID_Counter' />
                     <Value Type='Number'>" + angkadeleted + "</Value></Gt></Where>"
                };
                var a = objSPList.GetItems(query);
                if (a != null)
                {
                    foreach (SPListItem b in a)
                    {
                        b["ID_Counter"] = Convert.ToInt32(Convert.ToInt32(b["ID_Counter"]) - 1);
                        b.Update();
                    }
                }
            }
        }


    }
}