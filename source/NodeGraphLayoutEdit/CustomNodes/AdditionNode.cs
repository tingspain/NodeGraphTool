﻿/*

Copyright (c) 2011, Thomas ICHE
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the 
following conditions are met:

        * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
        * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer 
          in the documentation and/or other materials provided with the distribution.
        * Neither the name of PeeWeeK.NET nor the names of its contributors may be used to endorse or promote products derived from this 
          software without specific prior written permission.


THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, 
OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS;
OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT 
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/
using System;
using System.Collections.Generic;
using System.Text;
using NodeGraphControl;
using NodeGraphControl.Xml;

namespace NodeGraphLayoutEdit.CustomNodes
{
    public class AdditionNode:NodeGraphNode
    {
        public AdditionNode(int p_X, int p_Y, NodeGraphView p_View, bool p_CanBeSelected)
            : base(p_X, p_Y, p_View, p_CanBeSelected)
        {
            this.m_sName = "A+B";
            this.m_Connectors.Add(new NodeGraphConnector("A", this, ConnectorType.InputConnector, 0, "Float"));
            this.m_Connectors.Add(new NodeGraphConnector("B", this, ConnectorType.InputConnector, 1, "Float"));
            this.m_Connectors.Add(new NodeGraphConnector("Result (A+B)", this, ConnectorType.OutputConnector, 0, "Float"));
            this.Height = 64;
            
        }

        public AdditionNode(XmlTreeNode p_TreeNode, NodeGraphView p_View)
            : base(p_TreeNode, p_View)
        {
            this.m_sName = "A+B";
            this.m_Connectors.Add(new NodeGraphConnector("A", this, ConnectorType.InputConnector, 0, "Float"));
            this.m_Connectors.Add(new NodeGraphConnector("B", this, ConnectorType.InputConnector, 1, "Float"));
            this.m_Connectors.Add(new NodeGraphConnector("Result (A+B)", this, ConnectorType.OutputConnector, 0, "Float"));
            this.Height = 64;
        }

        public override NodeGraphData Process()
        {
            NodeGraphData v_InA = m_Connectors[0].Process();
            NodeGraphData v_InB = m_Connectors[1].Process();
            NodeGraphInvalidData v_OutError = new NodeGraphInvalidData();
            if (v_InA is NodeGraphInvalidData) v_OutError.Merge(v_InA as NodeGraphInvalidData);
            if (v_InB is NodeGraphInvalidData) v_OutError.Merge(v_InB as NodeGraphInvalidData);
            if (v_OutError.InvalidNodes.Count > 0) return v_OutError;

            float A = (m_Connectors[0].Process() as Data.NodeGraphFloatData).Value;
            float B = (m_Connectors[1].Process() as Data.NodeGraphFloatData).Value;
            return new Data.NodeGraphFloatData(A + B);
        }
    }
}
