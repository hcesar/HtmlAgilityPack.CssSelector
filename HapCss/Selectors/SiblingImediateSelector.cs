using HtmlAgilityPack;

namespace HapCss.Selectors;

internal class SiblingImediateSelector : CssSelector
{
    public override bool AllowTraverse => false;

    public override string Token => "+";

    protected internal override IEnumerable<HtmlNode> FilterCore(IEnumerable<HtmlNode> currentNodes)
    {
        foreach (HtmlNode node in currentNodes)
        {
            int idx = node.GetIndexOnParent();
            HtmlNode n = node.ParentNode.ChildNodes.Where(i => i.NodeType == HtmlNodeType.Element).Skip(idx + 1).FirstOrDefault();

            if (n != null)
                yield return n;
        }
    }
}
