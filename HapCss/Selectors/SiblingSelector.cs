using HtmlAgilityPack;

namespace HapCss.Selectors;

internal class SiblingSelector : CssSelector
{
    public override bool AllowTraverse => false;

    public override string Token => "~";

    protected internal override IEnumerable<HtmlNode> FilterCore(IEnumerable<HtmlNode> currentNodes)
    {
        foreach (HtmlNode node in currentNodes)
        {
            int idx = node.GetIndexOnParent();
            foreach (HtmlNode n in node.ParentNode.ChildNodes.Where(i => i.NodeType == HtmlNodeType.Element).Skip(idx + 1))
                yield return n;
        }
    }
}
