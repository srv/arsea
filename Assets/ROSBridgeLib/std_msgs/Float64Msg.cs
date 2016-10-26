using System.Collections;
using System.Text;
using SimpleJSON;
using UnityEngine;

namespace ROSBridgeLib {
	namespace std_msgs {
		public class Float64Msg : ROSBridgeMsg {
			private double _data;
			
			public Float64Msg(JSONNode msg) {
				//Debug.Log ("Float64Msg with " + msg.ToString());
				_data = double.Parse(msg);
			}
			
			public Float64Msg(double data) {
				_data = data;
			}
			
			public static string GetMessageType() {
				return "std_msgs/Float64";
			}
			
			public double GetData() {
				return _data;
			}
			
			public override string ToString() {
				return "Float64 [data=" + _data.ToString() + "]";
			}
			
			public override string ToYAMLString() {
				return "{\"data\" : " + _data.ToString() + "}";
			}
		}
	}
}