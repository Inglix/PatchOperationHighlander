using System.Linq;
using System.Xml;
using Verse;

namespace PatchOperation
{
    public class Set : Verse.PatchOperationPathed
    {
        XmlContainer value;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            XmlNode node = value.node;
			bool result = false;
            foreach (var xmlNode in xml.SelectNodes(this.xpath).Cast<XmlNode>())
            {
                result = true;

                foreach (XmlNode childNode in node.ChildNodes)
                {
                    var conflict = xmlNode.ChildNodes
                                    .OfType<XmlNode>()
                                    .FirstOrDefault(n => n.Name == childNode.Name);
                    var import = xmlNode.OwnerDocument.ImportNode(childNode, true);
                    if (conflict != null)
                    {
                        xmlNode.ReplaceChild(import, conflict);
                    }
                    else
                    {
                        xmlNode.AppendChild(import);
                    }
                }
            }
            return result;
        }
    }
}