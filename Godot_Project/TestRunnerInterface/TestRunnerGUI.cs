using System.Collections.Generic;
using Godot;
using GodotTestTools;

public class TestRunnerGUI : Tree
{
    [Export] bool enabled;

    Dictionary<string, TreeItem> treeItems = new Dictionary<string, TreeItem>();
    Dictionary<TreeItem, TestResult> itemSelections = new Dictionary<TreeItem, TestResult>();
    RichTextLabel richTextLabel;
    int passed = 0;
    int failed = 0;


    async public override void _Ready()
    {
        if (!enabled) return;

        TreeItem rootItem = CreateItem(this) as TreeItem;
        TestRunner testRunner = new TestRunner();

        passed = 0;
        failed = 0;
        richTextLabel = GetParent().GetChild(0) as RichTextLabel;

        await testRunner.Run((TestResult testResult) =>
        {
            bool didFail = testResult.result == TestResult.Result.Failed;

            if (didFail)
            {
                failed++;
            }
            else
            {
                passed++;
            }

            string classType = testResult.classType.ToString();

            if (!treeItems.ContainsKey(classType))
            {
                treeItems[classType] = CreateTreeItemsForClassType(classType, rootItem);
            }

            treeItems[classType].SetText(0, testResult.classType.ToString());

            TreeItem testItem = CreateItem(treeItems[classType]) as TreeItem;
            testItem.SetText(0, testResult.testMethod.Name);
            testItem.SetCustomColor(0, didFail ? new Color(1,0,0) : new Color(0,1,0));

            if (didFail)
            {
                TreeItem top = treeItems[classType];

                while (top != null)
                {
                    top.SetCustomColor(0, new Color(1,0,0));
                    top = top.GetParent();
                }
            }

            itemSelections[testItem] = testResult;
        });

        if (string.IsNullOrEmpty(richTextLabel.Text))
        {
            SetPassedFailedText();
        }

        Connect("cell_selected", this, "_CellSelected");
    }


    TreeItem CreateTreeItemsForClassType(string classType, TreeItem rootItem)
    {
        string[] treeItemParts = classType.Split('.');
        string currentClassType = "";
        TreeItem currentRoot = rootItem;

        for ( int i = 0; i < treeItemParts.Length; i++)
        {
            if (i > 0)
            {
                currentClassType += ".";
            }

            currentClassType += treeItemParts[i];

            if (!treeItems.ContainsKey(currentClassType))
            {
                treeItems[currentClassType] = CreateItem(currentRoot, 0) as TreeItem;
                treeItems[currentClassType].SetText(0, treeItemParts[i]);
                treeItems[currentClassType].SetCustomColor(0, new Color(0,1,0));
                treeItems[currentClassType].Collapsed = i == treeItemParts.Length - 1;
            }

            currentRoot = treeItems[currentClassType];
        }

        return currentRoot;
    }


    void SetPassedFailedText()
    {
        richTextLabel.BbcodeText = string.Format("[color=green]Passed: {0}[/color]\t\t[color=red]Failed: {1}[/color]", passed, failed);
    }


    void _CellSelected()
    {
        SetPassedFailedText();
        richTextLabel.BbcodeText += "\n\n" + itemSelections[GetSelected()].exception.Message;
    }
}