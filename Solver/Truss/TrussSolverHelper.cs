using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solver.Truss
{
    public enum eBenchmarkTests
    {
        Test1,
        Test2,
        Test3,
        Test4,
        Test5
    }
    public class TrussSolverHelper
    {
        #region Ctor
        public TrussSolverHelper()
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
        public void AddNode(int NodeID, double Xcoord, double Ycoord)
        {
            _nodeList.Add(new TrussNode(NodeID, Xcoord, Ycoord));
        }

        public void AddMember(int memberLabel, int nodeI, int nodeJ, double E, double Area)
        {
            var nodei = GetNodeById(nodeI);
            var nodej = GetNodeById(nodeJ);
            _elementList.Add(new TrussElement(memberLabel, nodei, nodej, E, Area));
        }
        public void AddLoad(int nodeId, double fx, double fy)
        {
            var node = GetNodeById(nodeId);
            node.Fx = fx;
            node.Fy = fy;

        }

        public void AddRestrainedNode(int nodeId, bool isXRestrained, bool isYRestrained)
        {
            var node = GetNodeById(nodeId) as TrussNode;

            if (node != null)
            {
                node.XRestraint = isXRestrained ? eRestraint.Restrained : eRestraint.Free;
                node.YRestraint = isYRestrained ? eRestraint.Restrained : eRestraint.Free;
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

        public void SetExample(eBenchmarkTests test)
        {
            ClearNodeAndElements();
            switch (test)
            {
                case eBenchmarkTests.Test1:
                    CreateExample1();
                    //CreateExample6();
                    break;
                case eBenchmarkTests.Test2:
                    CreateExample2();
                    break;
                case eBenchmarkTests.Test3:
                    CreateExample3();
                    break;
                case eBenchmarkTests.Test4:
                    CreateExample4();
                    break;
                case eBenchmarkTests.Test5:
                    CreateExample5();
                    break;
                default:
                    CreateExample5();
                    break;
            }
        }
        private void CreateExample1()
        {
            AddNode(1, 0, 0);
            AddNode(2, 5, 8.66);
            AddNode(3, 15, 8.66);
            AddNode(4, 20, 0);
            AddNode(5, 10, 0);
            AddNode(6, 10, -5);
            var E = 200 * Math.Pow(10, 9);
            var A = 5000;
            AddMember(1, 1, 2, E, A);
            AddMember(2, 2, 3, E, A);
            AddMember(3, 3, 4, E, A);
            AddMember(4, 4, 5, E, A);
            AddMember(5, 1, 5, E, A);
            AddMember(6, 2, 5, E, A);
            AddMember(7, 3, 5, E, A);
            AddMember(8, 5, 6, E, A);
            AddRestrainedNode(4, true, true);
            AddRestrainedNode(6, true, true);
            AddLoad(1, 0, -200000);

        }
        private void CreateExample2()
        {
            AddNode(1, 0, 0);
            AddNode(2, 1, 0);
            AddNode(3, 0.5, 1);
            var E = 1 * Math.Pow(10, 6);
            var A = 10000;
            AddMember(1, 1, 2, E, A);
            AddMember(2, 2, 3, E, A);
            AddMember(3, 3, 1, E, A);
            AddRestrainedNode(1, true, true);
            AddRestrainedNode(2, false, true);
            AddLoad(3, 0, -20);


        }
        private void CreateExample3()
        {
            var E = 2 * Math.Pow(10, 11);
            var A = 5000;

            AddNode(1, 0, 6);
            AddNode(2, 4, 6);
            AddNode(3, 8, 6);
            AddNode(4, 12, 6);
            AddNode(5, 16, 6);
            AddNode(6, 12, 2);
            AddNode(7, 8, 0);
            AddNode(8, 4, 2);

            AddMember(1, 1, 2, E, A);
            AddMember(2, 2, 3, E, A);
            AddMember(3, 3, 4, E, A);
            AddMember(4, 4, 5, E, A);
            AddMember(5, 5, 6, E, A);
            AddMember(6, 6, 7, E, A);
            AddMember(7, 7, 8, E, A);
            AddMember(8, 1, 8, E, A);
            AddMember(9, 2, 8, E, A);
            AddMember(10, 3, 7, E, A);
            AddMember(11, 4, 6, E, A);
            AddMember(12, 3, 8, E, A);
            AddMember(13, 3, 6, E, A);


            AddRestrainedNode(1, true, true);
            AddRestrainedNode(5, false, true);

            AddLoad(2, 0, -10000);
            AddLoad(3, 0, -30000);
            AddLoad(4, 0, -5000);
        }
        private void CreateExample4()
        {
            var E = 200 * Math.Pow(10, 9);
            var A = 5000;
            int node = 0;
            AddNode(++node, 0, 0);
            AddNode(++node, 10, 10);
            AddNode(++node, 20, 8.333);
            AddNode(++node, 30, 6.667);
            AddNode(++node, 40, 5);
            AddNode(++node, 50, 3.333);
            AddNode(++node, 60, 1.667);
            AddNode(++node, 70, 3.333);
            AddNode(++node, 80, 1.667);
            AddNode(++node, 90, 3.333);
            AddNode(++node, 100, 5);
            AddNode(++node, 110, 6.667);
            AddNode(++node, 120, 8.333);
            AddNode(++node, 130, 10);
            AddNode(++node, 140, 0);
            AddNode(++node, 130, 0);
            AddNode(++node, 120, 0);
            AddNode(++node, 110, 0);
            AddNode(++node, 100, 0);
            AddNode(++node, 90, 0);
            AddNode(++node, 80, 0);
            AddNode(++node, 70, 0);
            AddNode(++node, 60, 0);
            AddNode(++node, 50, 0);
            AddNode(++node, 40, 0);
            AddNode(++node, 30, 0);
            AddNode(++node, 20, 0);
            AddNode(++node, 10, 0);
            int member = 0;
            for (int i = 1; i <= 27; i++)
            {
                if (i != 15)
                {
                    AddMember(++member, i, i + 1, E, A);
                }
            }
            int end = 28;
            for (int i = 2; i <= 14; i++)
            {
                AddMember(member, i, end, E, A);
                member++;
                end--;
            }

            AddMember(++member, 3, 28, E, A);
            AddMember(++member, 4, 27, E, A);
            AddMember(++member, 5, 26, E, A);
            AddMember(++member, 6, 25, E, A);
            AddMember(++member, 7, 24, E, A);
            AddMember(++member, 7, 22, E, A);
            AddMember(++member, 9, 22, E, A);
            AddMember(++member, 9, 20, E, A);
            AddMember(++member, 10, 19, E, A);
            AddMember(++member, 11, 18, E, A);
            AddMember(++member, 12, 17, E, A);
            AddMember(++member, 13, 16, E, A);

            AddRestrainedNode(1, true, true);
            AddRestrainedNode(15, true, true);
            AddRestrainedNode(16, true, true);
            AddRestrainedNode(28, true, true);
            for (int i = 17; i <= 27; i++)
            {
                AddLoad(i, 0, -40000);
            }

        }
        private void CreateExample5()
        {
            var E = 200 * Math.Pow(10, 9);
            var A = 5000;
            int node = 0;
            AddNode(++node, 0, 0);
            AddNode(++node, 9.804, 18);
            AddNode(++node, 20, 18);
            AddNode(++node, 30.195, 18);
            AddNode(++node, 40, 18);
            AddNode(++node, 50.715, 18);
            AddNode(++node, 62.939, 18);
            AddNode(++node, 76.204, 18);
            AddNode(++node, 90, 18);
            AddNode(++node, 103.79, 18);
            AddNode(++node, 117.06, 18);
            AddNode(++node, 129.28, 18);
            AddNode(++node, 140, 18);
            AddNode(++node, 149.8, 18);
            AddNode(++node, 160, 18);
            AddNode(++node, 170.19, 18);
            AddNode(++node, 180, 0);
            AddNode(++node, 170.16, 5.194);
            AddNode(++node, 160, 6.948);
            AddNode(++node, 149.8, 5.194);
            AddNode(++node, 140, 0);
            AddNode(++node, 129.28, 4.939);
            AddNode(++node, 117.06, 8.61);
            AddNode(++node, 103.79, 10.87);
            AddNode(++node, 90, 11.633);
            AddNode(++node, 76.204, 10.87);
            AddNode(++node, 62.939, 8.61);
            AddNode(++node, 50.715, 4.939);
            AddNode(++node, 40, 0);
            AddNode(++node, 30.195, 5.194);
            AddNode(++node, 20, 6.948);
            AddNode(++node, 9.804, 5.194);
            int member = 0;
            for (int i = 1; i <= 31; i++)
            {
                AddMember(++member, i, i + 1, E, A);
            }
            int end = 32;
            for (int i = 2; i <= 16; i++)
            {
                AddMember(member, i, end, E, A);
                member++;
                end--;
            }

            AddMember(++member, 1, 32, E, A);
            AddMember(++member, 3, 32, E, A);
            AddMember(++member, 3, 30, E, A);
            AddMember(++member, 4, 29, E, A);
            AddMember(++member, 6, 29, E, A);
            AddMember(++member, 7, 28, E, A);
            AddMember(++member, 8, 27, E, A);
            AddMember(++member, 9, 26, E, A);
            AddMember(++member, 9, 24, E, A);
            AddMember(++member, 10, 23, E, A);
            AddMember(++member, 11, 22, E, A);
            AddMember(++member, 12, 21, E, A);
            AddMember(++member, 14, 21, E, A);
            AddMember(++member, 15, 20, E, A);
            AddMember(++member, 15, 18, E, A);

            AddRestrainedNode(1, true, true);
            AddRestrainedNode(17, true, true);
            AddRestrainedNode(21, true, true);
            AddRestrainedNode(29, true, true);

            AddLoad(6, 0, -200000);
            AddLoad(7, 0, -200000);
            AddLoad(8, 0, -200000);
            AddLoad(9, 0, -200000);
            AddLoad(10, 0, -200000);
            AddLoad(11, 0, -200000);
            AddLoad(12, 0, -200000);

        }

        private void CreateExample6()
        {
            AddNode(1, 0, 0);
            AddNode(2, 3, 0);
            AddNode(3, 0, 4);
            var E = 1;//200 * Math.Pow(10, 9);
            var A = 1;
            AddMember(1, 1, 2, E, A);
            AddMember(2, 2, 3, E, A);
            AddRestrainedNode(1, true, true);
            AddRestrainedNode(3, true, true);
            AddLoad(2, 0, -150000);

        }

        #endregion
    }
}
