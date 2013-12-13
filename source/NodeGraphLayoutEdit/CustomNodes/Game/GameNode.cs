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
using System.ComponentModel;
using System.Text;
using NodeGraphControl;
using NodeGraphControl.Xml;

namespace NodeGraphLayoutEdit.CustomNodes
{
    public class GameNode : NodeGraphNode
    {
        const string NAME = "Node";


        public GameNode(int p_X, int p_Y, NodeGraphView p_View, bool p_CanBeSelected)
            : base(p_X, p_Y, p_View, p_CanBeSelected)
        {
            this.m_sName = NAME;
            this.m_Connectors.Add(new NodeGraphConnector("Parent", this, ConnectorType.InputConnector, 0, "String"));
            this.m_Connectors.Add(new NodeGraphConnector("Child(s)", this, ConnectorType.OutputConnector, 0, "String"));
            this.Height = 45;

        }
        public GameNode(XmlTreeNode p_TreeNode, NodeGraphView p_View)
            : base(p_TreeNode, p_View)
        {
            this.m_sName = NAME;
            this.m_Connectors.Add(new NodeGraphConnector("Parent", this, ConnectorType.InputConnector, 0, "String"));
            this.m_Connectors.Add(new NodeGraphConnector("Child(s)", this, ConnectorType.OutputConnector, 0, "String"));
            this.Height = 45;

            DeserializeFromXML(p_TreeNode);
        }


        [Category("NodeGraphNode")]
        public string entity_filename
        {
            get;
            set;
        }

        [Category("NodeGraphNode")]
        public string graphics_filename
        {
            get;
            set;
        }
        

        //Stupid nondynamic serialization
        public override NodeGraphControl.Xml.XmlTreeNode SerializeToXML(NodeGraphControl.Xml.XmlTreeNode p_Parent)
        {
            NodeGraphControl.Xml.XmlTreeNode v_Out = base.SerializeToXML(p_Parent);
            v_Out.AddParameter("entity_filename", entity_filename);
            v_Out.AddParameter("graphics_filename", graphics_filename);
            return v_Out;
        }

        public void DeserializeFromXML(NodeGraphControl.Xml.XmlTreeNode p_TreeNode)
        {
            entity_filename = p_TreeNode.m_attributes["entity_filename"];
            graphics_filename = p_TreeNode.m_attributes["graphics_filename"];
        }


        public override NodeGraphData Process()
        {
            return new Data.NodeGraphStringData(entity_filename);
        }


        protected override string GetName()
        {
            return !String.IsNullOrEmpty(entity_filename) ? entity_filename : "! UNDEFINED NODE !";
        }
    }
}
