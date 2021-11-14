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
        
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        
        [Authorize]
        public async Task<IActionResult> Territory()
        {
            var territories = await _netzweltService.GetTerritory();

            var treeView = GenerateTreeView(territories.Data, null);

            var htmlTreeView = GenerateHtmlTreeView(treeView, "", false);

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

        private static string GenerateHtmlTreeView(List<TreeNode> list, string parentName, bool isNested)
        {
            string retString = "<ul class=\"list-group\">";

            if (isNested)
            {
                retString = $"<ul class=\"nested nav-list collapse\" id=\"{parentName}\">";
            }

            foreach (var node in list)
            {
                if (node.ChildNodes.Any())
                {
                    retString += $"<li class=\"nav-header list-group-item\" data-toggle=\"collapse\" data-target=\"#ul{node.Name.Replace(" ", "")}\"><span class=\"caret\"></span>  {node.Name}</li>";
                    retString += GenerateHtmlTreeView(node.ChildNodes, ("ul" + node.Name.Replace(" ", "")), true);
                }
                else
                {
                    retString += $"<li class=\"list-group-item\">{node.Name}</li>";
                }
            }

            retString += "</ul>";

            return retString;
        }
    }
}