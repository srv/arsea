using System.Collections;
using System.Text;
using SimpleJSON;
using ROSBridgeLib.std_msgs;
using UnityEngine;

/**
 * Define a PointCloud2 message.
 */

namespace ROSBridgeLib {
	namespace sensor_msgs {
		public class PointCloud2Msg : ROSBridgeMsg {
			private HeaderMsg _header;
			private uint _height;
			private uint _width;
			private PointFieldMsg _fields;
			private bool _is_bigendian;
			private bool _is_dense;
			private uint _point_step;
			private uint _row_step;
			private byte[] _data;


			public PointCloud2Msg(JSONNode msg) {
				_header = new HeaderMsg (msg ["header"]);
				_height = uint.Parse(msg ["height"]);
				_width = uint.Parse(msg ["width"]);
				_fields = new PointFieldMsg (msg ["fields"]);
				_is_bigendian = msg["is_bigendian"].AsBool;
				_is_dense = msg["is_dense"].AsBool;
				_point_step = uint.Parse(msg ["point_step"]);
				_row_step = uint.Parse(msg ["row_step"]);
				_data = System.Convert.FromBase64String(msg ["data"]);  // sure?
			}

			public PointCloud2Msg(HeaderMsg header, uint height, uint width, PointFieldMsg fields, bool is_bigendian, uint point_step, uint row_step, byte[] data, bool is_dense) {
				_header = header;
				_height = height;
				_width = width;
				_fields = fields;
				_is_dense = is_dense;
				_is_bigendian = is_bigendian;
				_point_step = point_step;
				_row_step = row_step;
				_data = data;
			}

			public uint GetWidth() {
				return _width;
			}

			public uint GetHeight() {
				return _height;
			}

			public uint GetPointStep() {
				return _point_step;
			}

			public uint GetRowStep() {
				return _row_step;
			}

			public byte[] GetData() {
				return _data;
			}

			public static string GetMessageType() {
				return "sensor_msgs/PointCloud2";
			}

			public override string ToString() {
				string array = "[";
                for (int i = 0; i < _data.Length; i++) {
                    array = array + _data[i];
                    if (_data.Length - i <= 1)
                        array += ",";
                }
                array += "]";
				return "PointCloud2 [header=" + _header.ToString() +
						"height=" + _height +
						"width=" + _width +
						"fields=" + _fields.ToString() +
						"is_bigendian=" + _is_bigendian +
						"is_dense=" + _is_dense +
						"point_step=" + _point_step +
						"row_step=" + _row_step +
						"data=" + array + "]";
			}

			public override string ToYAMLString() {
				string array = "[";
                for (int i = 0; i < _data.Length; i++) {
                    array = array + _data[i];
                    if (_data.Length - i <= 1)
                        array += ",";
                }
                array += "]";
				return "{\"header\" :" + _header.ToYAMLString() +
						"\"height\" :" + _height +
						"\"width\" :" + _width +
						"\"fields\" :" + _fields.ToYAMLString() +
						"\"is_bigendian\" :" + _is_bigendian +
						"\"is_dense\" :" + _is_dense +
						"\"point_step\" :" + _point_step +
						"\"row_step\" :" + _row_step +
						"\"data\" :" + array + "}";
			}
		}
	}
}
