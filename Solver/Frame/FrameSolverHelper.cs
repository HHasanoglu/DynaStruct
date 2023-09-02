using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver.Frame
{
    public class FrameSolverHelper
    {
        #region Ctor
        public FrameSolverHelper()
        {
            _nodeList = new List<ANode>();
            _elementList = new List<IElement>();
        }

        #endregion

        #region Private Fields

        private List<ANode> _nodeList;
        private List<IElement> _elementList;

        #endregion

        #region Private Methods

        #endregion

        #region Public Properties

        public List<ANode> NodeList { get => _nodeList; }
        public List<IElement> ElementList { get => _elementList; }

        #endregion

        #region Public Methods
        public void AddFrameNode(int NodeID, double Xcoord, double Ycoord)
        {
            _nodeList.Add(new FrameNode(NodeID, Xcoord, Ycoord));
        }

        public void AddFrameMember(int memberLabel, int nodeI, int nodeJ, double E, double Area, double I)
        {
            var nodei = GetNodeById(nodeI);
            var nodej = GetNodeById(nodeJ);
            _elementList.Add(new FrameElement(memberLabel, nodei, nodej, E, Area, I));
        }
        public void AddLoad(int nodeId, double fx, double fy)
        {
            var node = GetNodeById(nodeId);
            node.Fx = fx;
            node.Fy = fy;

        }

        public void AddRestrainedNode(int nodeId, bool isXRestrained, bool isYRestrained, bool isMomentRestrained)
        {
            var node = GetNodeById(nodeId) as FrameNode;

            if (node != null)
            {
                node.XRestraint = isXRestrained ? eRestraint.Restrained : eRestraint.Free;
                node.YRestraint = isYRestrained ? eRestraint.Restrained : eRestraint.Free;
                node.RotationRestraint = isMomentRestrained ? eRestraint.Restrained : eRestraint.Free;
            }
        }

        public ANode GetNodeById(int NodeId)
        {
            return _nodeList.FirstOrDefault(x => x.ID == NodeId);
        }
        public void ClearNodeAndElements()
        {
            _nodeList.Clear();
            _elementList.Clear();
        }

        public bool AnalyzeModel()
        {
            var isAnalysisSuccesful = false;
            if (_elementList.Count > 0 && _nodeList.Count > 0)
            {

                Assembler assembler = new Assembler(_elementList, _nodeList);

                isAnalysisSuccesful = true;
            }

            return isAnalysisSuccesful;
        }

        public double GetMinValueForColorMap(eResultToShow type)
        {
            double minValue = 0;
            if (_nodeList != null)
            {
                switch (type)
                {
                    case eResultToShow.Dispx:
                        minValue = _nodeList.Min(x => Math.Abs(x.Dispx));
                        break;
                    case eResultToShow.Dispy:
                        minValue = _nodeList.Min(x => Math.Abs(x.Dispy));
                        break;
                    case eResultToShow.Dispxy:
                        minValue = _nodeList.Min(x => Math.Abs(x.Dispxy));
                        break;
                    default:
                        minValue = _nodeList.Min(x => Math.Abs(x.Dispy));
                        break;
                }
            }
            return minValue;
        }

        public double GetMaxValueForColorMap(eResultToShow type)
        {
            double maxValue = 0;
            if (_nodeList != null)
            {
                switch (type)
                {
                    case eResultToShow.Dispx:
                        maxValue = _nodeList.Max(x => Math.Abs(x.Dispx));
                        break;
                    case eResultToShow.Dispy:
                        maxValue = _nodeList.Max(x => Math.Abs(x.Dispy));
                        break;
                    case eResultToShow.Dispxy:
                        maxValue = _nodeList.Max(x => Math.Abs(x.Dispxy));
                        break;
                    default:
                        maxValue = _nodeList.Max(x => Math.Abs(x.Dispy));
                        break;
                }
            }
            return maxValue;
        }

        public void CreateFrameExample1()
        {
            var E = 200e3;
            var A = 30000;
            var I = 300e6;
            AddFrameNode(1, 0, 0);
            AddFrameNode(2, 0, 5000);
            AddFrameNode(3, 10000, 5000);

            AddFrameMember(1, 1, 2, E, A, I);
            AddFrameMember(2, 2, 3, E, A, I);
            AddRestrainedNode(1, true, true,true);
            AddRestrainedNode(3, false, true,false);
            AddLoad(2, 100000, 0);
        }

        #endregion
    }
}
