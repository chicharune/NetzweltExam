using System.Collections.Generic;
using System.Security.Permissions;

namespace NetzweltExam.Controllers
{
    public class TreeNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TreeNode> ChildNodes { get; set; }
    }
}