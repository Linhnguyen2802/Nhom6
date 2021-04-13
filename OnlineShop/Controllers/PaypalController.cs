
//using Models.EF;
//using OnlineShop.Helper;
//using OnlineShop.Models;
//using PayPal.Api;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Configuration = OnlineShop.Helper.Configuration;

//namespace OnlineShop.Controllers
//{
//    public class PaypalController : Controller
//    {
//        //
//        // GET: /Paypal/

//        public ActionResult Index()
//        {
//            return View();
//        }
//        public ActionResult PaymentWithPaypal(string Cancel = null)
//        {
//            //getting the apiContext as earlier
//            APIContext apiContext = Configuration.GetAPIContext();
//            try
//            {
//                string payerId = Request.Params["PayerID"];
//                if (string.IsNullOrEmpty(payerId))
//                {

//                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paypal/PaymentWithPayPal?";

//                    var guid = Convert.ToString((new Random()).Next(100000));

//                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
//                    var links = createdPayment.links.GetEnumerator();
//                    string paypalRedirectUrl = null;
//                    while (links.MoveNext())
//                    {
//                        Links lnk = links.Current;
//                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
//                        {

//                            paypalRedirectUrl = lnk.href;
//                        }
//                    }

//                    Session.Add(guid, createdPayment.id);
//                    return Redirect(paypalRedirectUrl);
//                }
//                else
//                {
//                    var guid = Request.Params["guid"];
//                    var executedPayment = ExecutePayment(apiContext, payerId,
//                   Session[guid] as string);
//                    if (executedPayment.state.ToLower() != "approved")
//                    {
//                        return View("FailureView");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Logger.Log("Error" + ex.Message);
//                return View("FailureView");
//            }
//            return View("SuccessView");
//        }
//        private PayPal.Api.Payment payment;
//        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
//        {

//            var itemList = new ItemList() { items = new List<Item>() };
//            var cart = Session[OnlineShop.Common.CommonConstants.CartSession];
//            if (cart != "")
//            {
//                List<Models.CartItem> cart1 = (List<Models.CartItem>)cart;
//                foreach (var item in cart1)
//                {
//                    itemList.items.Add(new Item()
//                    {
//                        name = item.Product.Name.ToString(),
//                        currency = "TK",
//                        price = item.Product.Price.ToString(),
//                        quantity = item.Product.Quantity.ToString(),
//                        sku = "sku"
//                    });
//                }

//                var payer = new Payer() { payment_method = "paypal" };

//                var redirUrls = new RedirectUrls()
//                {
//                    cancel_url = redirectUrl + "&Cancel=true",
//                    return_url = redirectUrl
//                };
//                var details = new Details()
//                {
//                    tax = "1",
//                    shipping = "1",
//                    subtotal = "5"
//                };
//                var amount = new Amount()
//                {
//                    currency = "USD",
//                    total = Session["SesTotal"].ToString(),
//                    details = details
//                };

//                var transactionList = new List<Transaction>();
//                transactionList.Add(new Transaction()
//                {
//                    description = "Transaction description.",
//                    invoice_number = "your invoice number",
//                    amount = amount,
//                    item_list = itemList
//                });
//                this.payment = new Payment()
//                {
//                    intent = "sale",
//                    payer = payer,
//                    transactions = transactionList,
//                    redirect_urls = redirUrls
//                };

//            }
//            //itemList.items.Add(new Item()
//            //{
//            //    name = "Item Name",
//            //    currency = "USD",
//            //    price = "5",
//            //    quantity = "1",
//            //    sku = "sku"
//            //});

//            return this.payment.Create(apiContext);

//        }


//        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
//        {
//            var paymentExecution = new PaymentExecution() { payer_id = payerId };
//            this.payment = new Payment() { id = paymentId };
//            return this.payment.Execute(apiContext, paymentExecution);
//        }

//    }
//}