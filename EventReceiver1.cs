using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace cobaeventlagi.EventReceiver1
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class EventReceiver1 : SPItemEventReceiver
    {
        /// <summary>
        /// An item is being added.
        /// </summary>
        public override void ItemAdding(SPItemEventProperties properties)
        {
            base.ItemAdding(properties);
            SPList coba2 = properties.Web.Lists["ListKaryawanBaru"];
            string Mycolumn = properties.Web.Lists["ListKaryawanBaru"].Fields["TipeKaryawanID"].InternalName;
            string InputValueAftr;
            try
            {
                InputValueAftr = properties.AfterProperties[Mycolumn].ToString().ToUpper();
            }
            catch (NullReferenceException)
            {
                InputValueAftr = null;
            }
            SPList coba = properties.Web.Lists["ListTipeKaryawan"];
            SPQuery q = new SPQuery();
            q.Query = "";
            q.ViewFields = "<FieldRef Name='TipeID' />";
            q.ViewFieldsOnly = true;
            SPListItemCollection items = coba.GetItems(q);
            bool ada = false;
            if (InputValueAftr != null)
            {
                foreach (SPListItem item in items)
                {
                    if (item["TipeID"].ToString().ToUpper() == InputValueAftr)
                    {
                        ada = true;
                    }
                }
                if (ada == false)
                {
                    properties.Status = SPEventReceiverStatus.CancelWithError;
                    properties.ErrorMessage = "Tipe Karyawan Salah";
                    properties.Cancel = true;
                }
            }
        }

        /// <summary>
        /// An item is being updated.
        /// </summary>
        public override void ItemUpdating(SPItemEventProperties properties)
        {
            base.ItemUpdating(properties);
            SPList coba2 = properties.Web.Lists["ListKaryawanBaru"];
            string Mycolumn = properties.Web.Lists["ListKaryawanBaru"].Fields["TipeKaryawanID"].InternalName;
            string InputValueAftr;
            try
            {
                InputValueAftr = properties.AfterProperties[Mycolumn].ToString().ToUpper();
            }
            catch (NullReferenceException)
            {
                InputValueAftr = null;
            }
            SPList coba = properties.Web.Lists["ListTipeKaryawan"];
            SPQuery q = new SPQuery();
            q.Query = "";
            q.ViewFields = "<FieldRef Name='TipeID' />";
            q.ViewFieldsOnly = true;
            SPListItemCollection items = coba.GetItems(q);
            bool ada = false;
            if (InputValueAftr != null)
            {
                foreach (SPListItem item in items)
                {
                    if (item["TipeID"].ToString().ToUpper() == InputValueAftr)
                    {
                        ada = true;
                    }
                }
                if (ada == false)
                {
                    properties.Status = SPEventReceiverStatus.CancelWithError;
                    properties.ErrorMessage = "Tipe Karyawan Salah";
                    properties.Cancel = true;
                }
            }
        }


    }
}