using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetzweltExam.Models;
using NetzweltExam.Services;

namespace NetzweltExam.Controllers
{
    public class HomeController : Controller
    {
        private readonly INetzweltService _netzweltService;

        public HomeController(INetzweltService netzweltService)
        {
            _netzweltService = netzweltService;
        }


        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("territory")]
        [Authorize]
        public async Task<IActionResult> Territory()
        {
            var territories = await _netzweltService.GetTerritory();

            var treeView = GenerateTreeView(territories.Data, null);

            var htmlTreeView = GenerateHtmlTreeView(treeView);

            ViewBag.HtmlTree = htmlTreeView;

            return View();
        }

        private static List<TreeNode> GenerateTreeView(IEnumerable<TerritoryModel> list, TreeNode parentNode)
        {
            List<TreeNode> treeViewList = new List<TreeNode>();

            var nodes = list.Where(x => parentNode == null ? x.Parent == null : x.Parent == parentNode.Id);
            foreach (var node in nodes)
            {
                TreeNode newNode = new TreeNode();
                newNode.Id = node.Id;
                newNode.Name = node.Name;
                newNode.ChildNodes = new List<TreeNode>();

                if (parentNode == null)
                {
                    treeViewList.Add(newNode);
                }
                else
                {
                    parentNode.ChildNodes.Add(newNode);
                }

                GenerateTreeView(list, newNode);
            }

            return treeViewList;
        }

        private static string GenerateHtmlTreeView(List<TreeNode> list)
        {
            string retString = "<ul>";

            foreach (var node in list)
            {
                retString += $"<li>{node.Name}</li>";
                if (node.ChildNodes.Any())
                {
                    retString += GenerateHtmlTreeView(node.ChildNodes);
                }
            }

            retString += "</ul>";

            return retString;
        }
    }
}