using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextGameEditor
{
    public class NodesToTGBFormatConvertor
    {
        string _startGroup = "{";
        string _endGroup = "}";
        string _divider = ";";

        public string Convert(List<ICanvasNode> nodes)
        {
            IndexNodes(nodes);
            string result = NodesToString(nodes);

            return result;
        }

        void IndexNodes(List<ICanvasNode> nodes)
        {
            int index = 0;
            foreach (ICanvasNode node in nodes)
            {
                node.ID = index;
                index++;
            }
        }

        string NodesToString(List<ICanvasNode> nodes)
        {
            string result = "";
            foreach(ICanvasNode node in nodes)
            {
                result += NodeToString(node);
                result += Environment.NewLine;
            }
            return result;
        }

        string NodeToString(ICanvasNode node)
        {
            string result = "";
            result += _startGroup;
            result += node.ID.ToString();
            result += TypeSpecificString(node);
            result += _endGroup;
            return result;
        }

        string TypeSpecificString(ICanvasNode node)
        {
            if (node is Story.Node)
                return StoryNodeToString((Story.Node)node);
            else
                return ConditionNodeToString((Condition.Node)node);
        }

        string ConditionNodeToString(Condition.Node node)
        {
            string res = ConditionsToString(node);
            res += NextIdsToString(node);
            return res;
        }

        string NextIdsToString(Condition.Node node)
        {
            string res = "";
            res += _startGroup;
            if (node.TrueBranch.Edge != null)
                res += node.TrueBranch.Edge.Descendant.ID.ToString();
            else
                res += "#";
            res += _divider;
            if (node.FalseBranch.Edge != null)
                res += node.FalseBranch.Edge.Descendant.ID.ToString();
            else res += "#";
            res += _endGroup;
            return res;
        }

        string ConditionsToString(Condition.Node node)
        {
            string res = "";

            res += _startGroup;
            foreach(Condition.ExtractBox v in node.Conditions)
            {
                if (v.Variable != null && v.Variable.Name.Length > 0)
                    res += VariableToText(v.Variable);
            }
            res += _endGroup;

            return res;
        }

        string StoryNodeToString(Story.Node node)
        {

            string res = _divider;
            string text = node.Text;
            if (text.Length == 0)
                text = "#";
            res += text;
            res += _startGroup;
            res += OptionsToString(node);
            res += _endGroup;
            return res;
        }

        string OptionsToString(Story.Node n)
        {
            string res = "";
            foreach (Story.Option o in n.Options)
            {
                if (o.Edge != null)
                    res += OptionToString(o);
            }
            return res;
        }

        string OptionToString(Story.Option o)
        {
            string res = "";
            
            {
                res += _startGroup;
                string text = o.Text;
                if (text.Length == 0)
                    text = "#";
                res += text;
                res += _divider;
                res += o.Edge.Descendant.ID;
                res += VariablesToText(o.InputVariable, o.OutputVariable);
                res += _endGroup;
            }
            return res;
        }

        string VariablesToText(Variable i, Variable o)
        {
            string res = "";

            res += VariableToText(i);
            res += VariableToText(o);

            return res;
        }

        string VariableToText(Variable v)
        {
            string res = "";

            res += _startGroup;
            if (v.Name.Length > 0)
                res += v.Name;
            else
                res += "#";
            res += _divider;
            if (v.State)
                res += "t";
            else
                res += "f";
            res += _endGroup;

            return res;
        }
    }
}
