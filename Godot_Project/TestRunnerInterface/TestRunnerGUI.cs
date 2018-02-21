using System.Collections.Generic;
using Godot;
using GodotTestTools;

public class TestRunnerGUI : Tree
{
    [Export] bool enabled;
    [Export] Texture passedIcon;
    [Export] Texture failedIcon;

    Dictionary<string, TreeItem> treeItems = new Dictionary<string, TreeItem>();
    Dictionary<TreeItem, TestResult> itemSelections = new Dictionary<TreeItem, TestResult>();
    Panel panel;
    RichTextLabel richTextLabel;
    Button hideButton;
    int passed = 0;
    int failed = 0;


    async public override void _Ready()
    {
        if (!enabled) return;

        TreeItem rootItem = CreateItem(this) as TreeItem;
        TestRunner testRunner = new TestRunner(GetTree());

        passed = 0;
        failed = 0;

        panel = GetParent() as Panel;
        richTextLabel = GetNodeByTypeInChildren<RichTextLabel>(GetParent());
        hideButton = GetNodeByTypeInChildren<Button>(GetParent().GetParent());

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
            testItem.SetIcon(0, didFail ? failedIcon : passedIcon);

            if (didFail)
            {
                TreeItem top = treeItems[classType];

                while (top != null)
                {
                    top.SetIcon(0, failedIcon);
                    top = top.GetParent();
                }
            }

            itemSelections[testItem] = testResult;
        });

        if (string.IsNullOrEmpty(richTextLabel.Text))
        {
            SetPassedFailedText();
        }

        hideButton.Connect("pressed", this, "_HidePressed");
        Connect("cell_selected", this, "_CellSelected");
    }


    T GetNodeByTypeInChildren<T>(Node parent) where T : Node
    {
        int childCount = parent.GetChildCount();

        for (int i = 0; i < childCount; i++)
        {
            if (parent.GetChild(i) is T result)
            {
                return result;
            }
        }

        return null;
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
                treeItems[currentClassType].SetIcon(0, passedIcon);
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


    void _HidePressed()
    {
        bool show = !panel.Visible;

        panel.Visible = show;

        hideButton.Text = show ? "H" : "S";
    }


    void _CellSelected()
    {
        SetPassedFailedText();
        richTextLabel.BbcodeText += "\n\n" + itemSelections[GetSelected()].exception.Message;
    }
}