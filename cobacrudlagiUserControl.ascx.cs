using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace cobacrudlagi.cobacrudlagi
{
    public partial class cobacrudlagiUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                tampil(1);
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tampil(1);
        }

        public void generatePager(int totalRowCount, int pageSize, int currentPage)
        {
            int totalLinkInPage = 5;
            int totalPageCount = (int)Math.Ceiling((decimal)totalRowCount / pageSize);
            int startPageLink = Math.Max(currentPage - (int)Math.Floor((decimal)totalLinkInPage / 2), 1);
            int lastPageLink = Math.Min(startPageLink + totalLinkInPage - 1, totalPageCount);
            if ((startPageLink + totalLinkInPage - 1) > totalPageCount)
            {
                lastPageLink = Math.Min(currentPage + (int)Math.Floor((decimal)totalLinkInPage / 2), totalPageCount);
                startPageLink = Math.Max(lastPageLink - totalLinkInPage + 1, 1);
            }
            List<ListItem> pageLinkContainer = new List<ListItem>();
            if (startPageLink != 1)
                pageLinkContainer.Add(new ListItem("First", "1", currentPage != 1));
            for (int i = startPageLink; i <= lastPageLink; i++)
            {
                pageLinkContainer.Add(new ListItem(i.ToString(), i.ToString(), currentPage != i));
            }
            if (lastPageLink != totalPageCount)
                pageLinkContainer.Add(new ListItem("Last", totalPageCount.ToString(), currentPage != totalPageCount));

            dlPager.DataSource = pageLinkContainer;
            dlPager.DataBind();
        }

        protected void dlPager_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "PageNo")
            {
                tampil(Convert.ToInt32(e.CommandArgument));
                lblhal.Value = e.CommandArgument.ToString();
            }
        }

        protected void tampil(int halaman)
        {
            int pagesize = 3;
            lblhal.Value = halaman.ToString();
            DataTable dtFeedbacks = new DataTable();
            dtFeedbacks.Columns.Add("Title", typeof(string));
            dtFeedbacks.Columns.Add("ID_Karyawan", typeof(string));
            dtFeedbacks.Columns.Add("Nama", typeof(string));
            dtFeedbacks.Columns.Add("Tipe_Karyawan", typeof(string));
            using (SPSite objSPSite = new SPSite(SPContext.Current.Web.Url))
            {
                using (SPWeb objSPWeb = objSPSite.OpenWeb())
                {
                    //query untuk tipe karyawan
                    SPList objSPList2 = objSPWeb.Lists.TryGetList("ListTipeKaryawan");
                    var query2 = new SPQuery
                    {
                        Query = @"<OrderBy>
                              <FieldRef Name='TipeID' Ascending='True' />
                           </OrderBy>"
                    };
                    SPListItemCollection listItems2 = objSPList2.GetItems(query2);

                    //query untuk karyawan
                    SPList objSPList = objSPWeb.Lists.TryGetList("ListKaryawanBaru");
                    var query = new SPQuery
                    {
                        Query = @"<Where>
                                  <And>
                                     <Geq>
                                        <FieldRef Name='ID_Counter' />
                                        <Value Type='Number'>"+((halaman-1)*pagesize)+"</Value></Geq>"
                                     +"<Lt>"
                                        +"<FieldRef Name='ID_Counter' />"+
                                        "<Value Type='Number'>"+(pagesize+(pagesize*(halaman-1)))+"</Value>"
                                     +"</Lt>"
                                  +"</And>"+
                               "</Where>"
                            +"<OrderBy>"+
                              "< FieldRef Name = 'IDKaryawan_ListKaryawan' Ascending = 'True' />"+
                              "<FieldRef Name='ID_Counter' Ascending='True' />"
                           +"</OrderBy>"
                    };
                    SPListItemCollection listItems = objSPList.GetItems(query);
                    //query untuk ngitung total karyawan
                    SPList objSPList3 = objSPWeb.Lists.TryGetList("ListKaryawanBaru");
                    var query3 = new SPQuery
                    {
                        Query = @"<OrderBy>
                              <FieldRef Name='ID_Counter' Ascending='True' />
                           </OrderBy>"
                    };
                    SPListItemCollection listItems3 = objSPList3.GetItems(query3);

                    foreach (SPItem item in listItems)
                    {
                        DataRow dr = dtFeedbacks.NewRow();
                        dr["Title"] = item["Title"].ToString();
                        if (dr["Title"] == null)
                        {
                            dr["Title"] = "";
                        }
                        dr["ID_Karyawan"] = item["IDKaryawan_ListKaryawan"].ToString();
                        if (dr["ID_Karyawan"] == null)
                        {
                            dr["ID_Karyawan"] = "";
                        }
                        dr["ID_Karyawan"] = item["IDKaryawan_ListKaryawan"].ToString();
                        if (dr["ID_Karyawan"] == null)
                        {
                            dr["ID_Karyawan"] = "";
                        }
                        dr["Nama"] = item["Nama"].ToString();
                        if (dr["Nama"] == null)
                        {
                            dr["Nama"] = "";
                        }
                        //cocokin tipekaryawanid di list karyawan baru sama list tipe karyawan
                        bool coba = false;
                        for (var i = 0; i < listItems2.Count; i++)
                        {
                            if (item["TipeKaryawanID"].ToString() == listItems2[i]["TipeID"].ToString())
                            {
                                dr["Tipe_Karyawan"] = listItems2[i]["Keterangan"].ToString();
                                coba = true;
                                break;
                            }
                        }
                        if (coba == false)
                        {
                            dr["Tipe_Karyawan"] = "";
                        }
                        dtFeedbacks.Rows.Add(dr);
                    }
                    gvSelectedColumnListData.DataSource = dtFeedbacks;
                    gvSelectedColumnListData.DataBind();
                    generatePager(listItems3.Count, pagesize, halaman);
                }
            }
        }
        protected void rowcommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);            
            string code = gvSelectedColumnListData.DataKeys[index].Value.ToString();
            lblIndex.Value = code;
            if (e.CommandName.Equals("editRecord"))
            {
                try
                {
                    pilih.Value = "edit";
                    using (SPSite objSPSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb objSPWeb = objSPSite.OpenWeb())
                        {
                            SPList objSPList = objSPWeb.Lists.TryGetList("ListKaryawanBaru");
                            if (objSPList != null)
                            {
                                var query = new SPQuery
                                {
                                    Query = @"<Where>
                              <Eq>
                                 <FieldRef Name='IDKaryawan_ListKaryawan' />
                                 <Value Type='Text'>" + code + "</Value></Eq></Where>"
                                };
                                var a = objSPList.GetItems(query);
                                titleUpdate.Text = a[0]["Title"].ToString();
                                namaUpdate.Text = a[0]["Nama"].ToString();
                                IDUpdate.Text = a[0]["IDKaryawan_ListKaryawan"].ToString();
                                tipeIDUpdate.Text = a[0]["TipeKaryawanID"].ToString();
                                tampil(1);
                            }
                        }
                    }
                }
                catch
                {
                }
            }
            else
            if (e.CommandName.Equals("deleteRecord"))
            {
                try
                {
                    int id = Convert.ToInt32(code);

                    using (SPSite objSPSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb objSPWeb = objSPSite.OpenWeb())
                        {
                            bool allowUnsafeUpdate = objSPWeb.AllowUnsafeUpdates;
                            SPList objSPList = objSPWeb.Lists.TryGetList("ListKaryawanBaru");
                            if (objSPList != null)
                            {
                                objSPWeb.AllowUnsafeUpdates = true;
                                var query = new SPQuery
                                {
                                    Query = @"<Where>
                              <Eq>
                                 <FieldRef Name='IDKaryawan_ListKaryawan' />
                                 <Value Type='Text'>" + id.ToString() + "</Value></Eq></Where>"
                                };
                                var a = objSPList.GetItems(query);
                                a.Delete(0);
                                //lblDeleteStatus.Text = "Item with ID: " + id.ToString() + " deleted successfuly.";
                                objSPWeb.AllowUnsafeUpdates = allowUnsafeUpdate;
                                tampil(1);
                            }
                            //else
                            //{
                            //    lblDeleteStatus.Text = "The list is not exists.";
                            //}
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblHasil.Value = "";
                    //lblDeleteStatus.Text = ex.ToString();
                }
            }
        }
        //protected void isiUpdate(object sender, EventArgs e)
        //{
        //    if (pilih.Value == "edit")
        //    {
        //        using (SPSite objSPSite = new SPSite(SPContext.Current.Web.Url))
        //        {
        //            using (SPWeb objSPWeb = objSPSite.OpenWeb())
        //            {
        //                SPList objSPList = objSPWeb.Lists.TryGetList("ListIDKaryawan");
        //                if (objSPList != null)
        //                {
        //                    var query = new SPQuery
        //                    {
        //                        Query = @"<Where>
        //                      <Eq>
        //                         <FieldRef Name='IDKaryawan_ListIDKaryawan' />
        //                         <Value Type='Text'>" + indexGv.Value + "</Value></Eq></Where>"
        //                    };
        //                    var a = objSPList.GetItems(query);
        //                    titleUpdate.Text = a[0]["Title"].ToString();
        //                    IDUpdate.Text = a[0]["IDKaryawan_ListIDKaryawan"].ToString();
        //                }
        //            }
        //        }
        //    }
        //}
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SPSite objSPSite = new SPSite(SPContext.Current.Web.Url))
                {
                    using (SPWeb objSPWeb = objSPSite.OpenWeb())
                    {
                        bool allowUnsafeUpdate = objSPWeb.AllowUnsafeUpdates;
                        SPList objSPList = objSPWeb.Lists.TryGetList("ListKaryawanBaru");
                        if (objSPList != null)
                        {
                            objSPWeb.AllowUnsafeUpdates = true;
                            var query = new SPQuery
                            {
                                Query = @"<Where>
                              <Eq>
                                 <FieldRef Name='IDKaryawan_ListKaryawan' />
                                 <Value Type='Text'>" + lblIndex.Value + "</Value></Eq></Where>"
                            };
                            var a = objSPList.GetItems(query);
                            foreach (SPListItem b in a)
                            {
                                b["Title"] = titleUpdate.Text;
                                b["Nama"] = namaUpdate.Text;
                                b["IDKaryawan_ListKaryawan"] = IDUpdate.Text;
                                b["TipeKaryawanID"] = Convert.ToInt32(tipeIDUpdate.Text);
                                b.Update();
                            }
                            tampil(1);
                            pilih.Value = "";
                        }
                        else
                        {
                            lblResult.Text = "List tidak ditemukan";
                        }
                    }
                }
            }
            catch (SPException)
            {
                lblHasil.Value = "editsalah";
                pilih.Value = "edit";
                lblResultEdit.Text = "Tipe Karyawan Salah";
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTitle.Text.Trim() != "" && txtID.Text.Trim() != "")
                {
                    using (SPSite objSPSite = new SPSite(SPContext.Current.Web.Url))
                    {
                        using (SPWeb objSPWeb = objSPSite.OpenWeb())
                        {
                            SPList objSPList = objSPWeb.Lists.TryGetList("ListKaryawanBaru");
                            if (objSPList != null)
                            {
                                SPListItem objSPListItem = objSPList.Items.Add();
                                objSPListItem["Title"] = txtTitle.Text.Trim();
                                objSPListItem["Nama"] = txtNama.Text.Trim();
                                objSPListItem["IDKaryawan_ListKaryawan"] = txtID.Text.Trim();
                                objSPListItem["TipeKaryawanID"] = Convert.ToInt32(txtTipeKID.Text.Trim());
                                objSPListItem.Update();
                                Clear();
                                lblHasil.Value = "masuk";
                                tampil(1);
                            }
                            else
                            {
                                lblResult.Text = "List tidak ditemukan";
                            }
                        }
                    }
                }
                else
                {
                    lblResult.Text = "Data tidak boleh kosong";
                }
            }
            catch (SPException)
            {
                lblHasil.Value = "addsalah";
                lblResult.Text = "Tipe Karyawan Salah";
            }

        }
        void Clear()
        {
            txtTitle.Text = "";
            txtNama.Text = "";
            txtID.Text = "";
            txtTipeKID.Text = "";
        }

        //protected void Unnamed1_Click(object sender, EventArgs e)
        //{
        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.AddHeader(
        //        "content-disposition", string.Format("attachment; filename={0}", "Customers.xls"));
        //    HttpContext.Current.Response.ContentType = "application/ms-excel";

        //    using (StringWriter sw = new StringWriter())
        //    {
        //        using (HtmlTextWriter htw = new HtmlTextWriter(sw))
        //        {
        //            //  Create a table to contain the grid  
        //            Table table = new Table();

        //            //  add each of the data rows to the table  
        //            foreach (GridViewRow row in gvSelectedColumnListData.Rows)
        //            {
        //                table.Rows.Add(row);
        //            }

        //            //  render the table into the htmlwriter  
        //            table.RenderControl(htw);

        //            //  render the htmlwriter into the response  
        //            HttpContext.Current.Response.Write(sw.ToString());
        //            HttpContext.Current.Response.End();
        //        }
        //    }
        //}
    }
        
        //protected void btnDeleteListItem_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int id = Convert.ToInt32(hfCode.Value);

        //        using (SPSite objSPSite = new SPSite(SPContext.Current.Web.Url))
        //        {
        //            using (SPWeb objSPWeb = objSPSite.OpenWeb())
        //            {
        //                bool allowUnsafeUpdate = objSPWeb.AllowUnsafeUpdates;
        //                SPList objSPList = objSPWeb.Lists.TryGetList("ListIDKaryawan");
        //                if (objSPList != null)
        //                {
        //                    objSPWeb.AllowUnsafeUpdates = true;
        //                    var query = new SPQuery
        //                    {
        //                        Query = @"<Where>
        //                      <Eq>
        //                         <FieldRef Name='IDKaryawan_ListIDKaryawan' />
        //                         <Value Type='Text'>" + id.ToString() + "</Value></Eq></Where>"
        //                    };
        //                    var a = objSPList.GetItems(query);
        //                    a.Delete(0);
        //                    lblDeleteStatus.Text = "Item with ID: " + id.ToString() + " deleted successfuly.";
        //                    objSPWeb.AllowUnsafeUpdates = allowUnsafeUpdate;
        //                    Clear();
        //                    lblHasil.Value = "masuk";
        //                    tampil();
        //                }
        //                else
        //                {
        //                    lblDeleteStatus.Text = "The list is not exists.";
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblHasil.Value = "";
        //        lblDeleteStatus.Text = ex.ToString();
        //    }
        //}
    }
