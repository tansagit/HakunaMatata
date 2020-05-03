﻿using HakunaMatata.Models.DataModels;
using HakunaMatata.Models.ViewModels;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static HakunaMatata.Helpers.Constants;

namespace HakunaMatata.Helpers
{
    public class Helper
    {
        public static ClaimsPrincipal GenerateIdentity(Agent user)
        {
            string role;
            switch (user.LevelId)
            {
                case 1: role = "Admin"; break;
                case 2: role = "Manager"; break;
                case 3: role = "Customer"; break;
                default: role = "Customer"; break;
            }

            var claims = new List<Claim>()
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("UserName",user.AgentName),
                new Claim(ClaimTypes.Email , user.Email),
                new Claim("Phone",user.PhoneNumber),
                new Claim(ClaimTypes.Role , role)
            };

            var identity = new ClaimsIdentity(claims, "User Identity");
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }

        public static string ChangeImageURl(string name)
        {
            return "~/images/" + name;
        }

        /// <summary>
        /// render modal view
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string RenderRazorViewToString(Controller controller, string viewName, object model = null)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new HtmlHelperOptions()
                );
                viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }

        public static decimal[] GetPriceRange(int priceRange)
        {

            decimal min;
            decimal max;
            if (priceRange == 1) //duoi 1tr
            {
                min = 0;
                max = 1000000;
            }
            else if (priceRange == 2) //1tr-2tr
            {
                min = 1000000;
                max = 2000000;
            }
            else if (priceRange == 3) //2tr-3tr
            {
                min = 2000000;
                max = 3000000;
            }
            else if (priceRange == 4) //3tr-5tr
            {
                min = 3000000;
                max = 5000000;
            }
            else if (priceRange == 5) //5tr-7tr
            {
                min = 5000000;
                max = 7000000;
            }
            else if (priceRange == 6) //7tr-10tr
            {
                min = 7000000;
                max = 10000000;
            }
            else if (priceRange == 7) //tren 10tr
            {
                min = 10000000;
                max = decimal.MaxValue;
            }
            else //chon tat ca
            {
                min = 0;
                max = decimal.MaxValue;
            }
            var result = new decimal[] { min, max };

            return result;
        }

        public static int[] GetAcreageRange(int acreageRange)
        {

            int min;
            int max;
            if (acreageRange == 1) //duoi 20m2
            {
                min = 0;
                max = 20;
            }
            else if (acreageRange == 2) //20m2-30m2
            {
                min = 20;
                max = 30;
            }
            else if (acreageRange == 3) //30-50m2
            {
                min = 30;
                max = 50;
            }
            else if (acreageRange == 4) //50-60m2
            {
                min = 50;
                max = 60;
            }
            else if (acreageRange == 5) //60-70m2
            {
                min = 60;
                max = 70;
            }
            else if (acreageRange == 6) //70-80m2
            {
                min = 70;
                max = 80;
            }
            else if (acreageRange == 7) //80-90m2
            {
                min = 80;
                max = 90;
            }
            else if (acreageRange == 8) //90-100m2
            {
                min = 90;
                max = 100;
            }
            else if (acreageRange == 9) //tren 100m2
            {
                min = 100;
                max = int.MaxValue;
            }
            else //chon tat ca
            {
                min = 0;
                max = 999;
            }
            var result = new int[] { min, max };

            return result;
        }

        public static string GetStreet(string address)
        {
            return address.Split(",")[0];
        }

        public static List<VM_Search_Result> MapperToVMSearchResult(List<RealEstateDetail> searchResult)
        {

            var result = new List<VM_Search_Result>();
            if (searchResult != null || searchResult.ToList().Count > 0)
            {
                foreach (var item in searchResult)
                {
                    string imageUrl = string.Empty;

                    //neu list picture null hoac rong thi anh dai dien = 404
                    if (item.RealEstate.Picture == null || item.RealEstate.Picture.ToList().Count == 0)
                    {
                        imageUrl = "404";
                    }
                    else
                    {
                        //lay phan tu dau tien trong list picture
                        var img = item.RealEstate.Picture.ToList().First();

                        //kiem tra picture name no ton tai ko, 
                        // neu co nghia la moi them vao, ko phải crawl tu web
                        if (!string.IsNullOrEmpty(img.PictureName))
                        {
                            //tao url = cach + chuoi ~/images/ + PictureName
                            imageUrl = "local" + img.PictureName;
                        }
                        else
                        {
                            imageUrl = img.Url;
                        }
                    }

                    var resultItem = new VM_Search_Result()
                    {
                        Id = item.RealEstate.Id,
                        Street = GetStreet(item.RealEstate.Map.Address),
                        Price = item.Price,
                        Acreage = item.Acreage,
                        Type = item.RealEstate.RealEstateTypeId,
                        PostTime = item.RealEstate.PostTime.ToString("dd/MM/yyyy"),
                        ImageUrl = imageUrl,
                        AgentName = item.RealEstate.Agent.AgentName
                    };
                    result.Add(resultItem);
                }
            }

            return result;
        }

        public static List<SelectListItem> GetPriceRangeForView1()
        {
            List<SelectListItem> ranges = new List<SelectListItem>();
            ranges.Add(new SelectListItem() { Text = "Tất cả", Value = "0" });
            ranges.Add(new SelectListItem() { Text = "Dưới 1 triệu", Value = "1" });
            ranges.Add(new SelectListItem() { Text = "1 triệu - 2 triệu", Value = "2" });
            ranges.Add(new SelectListItem() { Text = "2 triệu - 3 triệu", Value = "3" });
            ranges.Add(new SelectListItem() { Text = "3 triệu - 5 triệu", Value = "4" });
            ranges.Add(new SelectListItem() { Text = "5 triệu - 7 triệu", Value = "5" });
            ranges.Add(new SelectListItem() { Text = "7 triệu - 10 triệu", Value = "6" });
            ranges.Add(new SelectListItem() { Text = "Trên 10 triệu", Value = "7" });

            return ranges;
        }

        public static Dictionary<string, int> GetPriceRangeForView()
        {
            var dictionary = new Dictionary<string, int>
            {
                {"Tất cả",0 },
                {"Dưới 1 triệu",1 },
                {"1 triệu - 2 triệu",2 },
                {"2 triệu - 3 triệu",3 },
                {"3 triệu - 5 triệu",4 },
                {"5 triệu - 7 triệu",5 },
                {"7 triệu - 10 triệu",6 },
                {"Trên 10 triệu",7 }
            };
            return dictionary;
        }

        public static List<SelectListItem> GetAcreageRangeForView1()
        {
            List<SelectListItem> ranges = new List<SelectListItem>();
            ranges.Add(new SelectListItem() { Text = "Tất cả", Value = "0" });
            ranges.Add(new SelectListItem() { Text = "Dưới 20m2", Value = "1" });
            ranges.Add(new SelectListItem() { Text = "20m2 - 30m2", Value = "2" });
            ranges.Add(new SelectListItem() { Text = "30m2 - 50m2", Value = "3" });
            ranges.Add(new SelectListItem() { Text = "50m2 - 60m2", Value = "4" });
            ranges.Add(new SelectListItem() { Text = "60m2 - 70m2", Value = "5" });
            ranges.Add(new SelectListItem() { Text = "70m2 - 80m2", Value = "6" });
            ranges.Add(new SelectListItem() { Text = "80m2 - 90m2", Value = "7" });
            ranges.Add(new SelectListItem() { Text = "90m2 - 100m2", Value = "8" });
            ranges.Add(new SelectListItem() { Text = "Trên 100m2", Value = "9" });

            return ranges;
        }
        public static Dictionary<string, int> GetAcreageRangeForView()
        {
            var dictionary = new Dictionary<string, int>
            {
                {"Tất cả",0 },
                {"Dưới 20m2",1 },
                {"20m2 - 30m2",2 },
                {"30m2 - 50m2",3 },
                { "50m2 - 60m2",4},
                { "60m2 - 70m2",5},
                {"70m2 - 80m2",6 },
                {"80m2 - 90m2",7 },
                {"90m2 - 100m2",8 },
                {"Trên 100m2",9 }
            };
            return dictionary;
        }
        #region ConvertHtmlToPlainText

        public static string ConvertHtmlToPlainText(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            StringWriter sw = new StringWriter();
            ConvertTo(doc.DocumentNode, sw);
            sw.Flush();
            return sw.ToString();
        }

        private static void ConvertContentTo(HtmlNode node, TextWriter outText)
        {
            foreach (HtmlNode subnode in node.ChildNodes)
            {
                ConvertTo(subnode, outText);
            }
        }
        private static void ConvertTo(HtmlNode node, TextWriter outText)
        {
            string html;
            switch (node.NodeType)
            {
                case HtmlNodeType.Comment:
                    // don't output comments
                    break;

                case HtmlNodeType.Document:
                    ConvertContentTo(node, outText);
                    break;

                case HtmlNodeType.Text:
                    // script and style must not be output
                    string parentName = node.ParentNode.Name;
                    if ((parentName == "script") || (parentName == "style"))
                        break;

                    // get text
                    html = ((HtmlTextNode)node).Text;

                    // is it in fact a special closing node output as text?
                    if (HtmlNode.IsOverlappedClosingElement(html))
                        break;

                    // check the text is meaningful and not a bunch of whitespaces
                    if (html.Trim().Length > 0)
                    {
                        outText.Write(HtmlEntity.DeEntitize(html));
                    }
                    break;

                case HtmlNodeType.Element:
                    switch (node.Name)
                    {
                        case "p":
                            // treat paragraphs as crlf
                            outText.Write("\r\n");
                            break;
                        case "br":
                            outText.Write("\r\n");
                            break;
                    }

                    if (node.HasChildNodes)
                    {
                        ConvertContentTo(node, outText);
                    }
                    break;
            }
        }
        #endregion
    }



}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class NoDirectAccessAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (filterContext.HttpContext.Request.GetTypedHeaders().Referer == null ||
            filterContext.HttpContext.Request.GetTypedHeaders().Host.Host.ToString() != filterContext.HttpContext.Request.GetTypedHeaders().Referer.Host.ToString())
        {
            //filterContext.HttpContext.Response.Redirect("/");
            filterContext.Result = new RedirectToRouteResult(new
                   RouteValueDictionary(new { area = "AdminArea", controller = "Home", action = "Index" }));
        }
    }
}

