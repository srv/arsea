using System.Collections;
using System.Text;
using SimpleJSON;
using ROSBridgeLib.std_msgs;
using ROSBridgeLib.geometry_msgs;
using ROSBridgeLib.geographic_msgs;

/**
 * Define a auv_msgs NavSts  message. This has been hand-crafted from the corresponding
 * auv_msgs message file.
 */

namespace ROSBridgeLib {
	namespace auv_msgs {
		public class NavStsMsg : ROSBridgeMsg {
			public HeaderMsg _header;
			public GeoPointMsg _global_position;
			public GeoPointMsg _origin;
			public NEDMsg _position;
			public Float32Msg _altitude;
			public PointMsg _body_velocity;
			public PointMsg _seafloor_velocity;
			public Vector3Msg _orientation;
			public Vector3Msg _orientation_rate;
			public NEDMsg _position_variance;
			public Vector3Msg _orientation_variance;
			public UInt8Msg _status;

			public NavStsMsg(JSONNode msg) {
                _header = new HeaderMsg(msg["header"]);
				_global_position = new GeoPointMsg(msg["global_position"]);
				_origin = new GeoPointMsg(msg["origin"]);
				_position = new NEDMsg(msg["position"]);
				_altitude = new Float32Msg(msg["altitude"]);
				_body_velocity = new PointMsg(msg["body_velocity"]);
				_seafloor_velocity = new PointMsg(msg["seafloor_velocity"]);
				_orientation = new Vector3Msg(msg["orientation"]);
				_orientation_rate = new Vector3Msg(msg["orientation_rate"]);
				_position_variance = new NEDMsg(msg["position_variance"]);
				_orientation_variance = new Vector3Msg(msg["orientation_variance"]);
				_status = new UInt8Msg(msg["status"]);
            }

			public NavStsMsg(HeaderMsg header, 
							 GeoPointMsg global_position, 
							 GeoPointMsg origin, 
							 NEDMsg position,
							 Float32Msg altitude,
							 PointMsg body_velocity,
							 PointMsg seafloor_velocity,
							 Vector3Msg orientation,
							 Vector3Msg orientation_rate,
							 NEDMsg position_variance,
							 Vector3Msg orientation_variance,
							 UInt8Msg status) {
                _header = header;
                _global_position = global_position;
				_origin = origin;
				_position = position;
				_altitude = altitude;
				_body_velocity = body_velocity;
				_seafloor_velocity = seafloor_velocity;
				_orientation = orientation;
				_orientation_rate = orientation_rate;
				_position_variance = position_variance;
				_orientation_variance = orientation_variance;
				_status = status;
            }
			
			public static string getMessageType() {
				return "auv_msgs/NavSts ";
			}

            public HeaderMsg GetHeader() {
                return _header;
            }

            public GeoPointMsg GetGlobalPosition()
            {
                return _global_position;
            }

            public GeoPointMsg GetOrigin()
            {
                return _origin;
            }

            public NEDMsg GetPosition() {
				return _position;
			}

            public Float32Msg GetAltitude()
            {
                return _altitude;
            }

            public PointMsg GetBodyVelocity()
            {
                return _body_velocity;
            }

            public PointMsg GetSeafloorVelocity()
            {
                return _seafloor_velocity;
            }

            public Vector3Msg GetOrientation() {
				return _orientation;
			}

            public Vector3Msg GetOrientationRate()
            {
                return _orientation_rate;
            }

            public NEDMsg GetPositionVariance()
            {
                return _position_variance;
            }

            public Vector3Msg GetOrientationVariance()
            {
                return _orientation_variance;
            }

            public UInt8Msg GetStatus()
            {
                return _status;
            }

            public override string ToString() {
				return "auv_msgs/NavSts  [header=" + _header.ToString() +
						", global_position=" + _global_position.ToString() +
						", origin=" + _origin.ToString() +
						", position=" + _position.ToString() +
						", altitude=" + _altitude.ToString() +
						", body_velocity=" + _body_velocity.ToString() +
						", seafloor_velocity=" + _seafloor_velocity.ToString() +
						", orientation=" + _orientation.ToString() +
						", orientation_rate=" + _orientation_rate.ToString() +
						", position_variance=" + _position_variance.ToString() +
						", orientation_variance=" + _orientation_variance.ToString() +
						", status=" + _status.ToString() + "]";
			}
			
			public override string ToYAMLString() {
				return "{\"header\":" + _header.ToYAMLString() +
						", \"global_position\":" + _global_position.ToYAMLString() +
						", \"origin\":" + _origin.ToYAMLString() +
						", \"position\":" + _position.ToYAMLString() +
						", \"altitude\":" + _altitude.ToYAMLString() +
						", \"body_velocity\":" + _body_velocity.ToYAMLString() +
						", \"seafloor_velocity\":" + _seafloor_velocity.ToYAMLString() +
						", \"orientation\":" + _orientation.ToYAMLString() +
						", \"orientation_rate\":" + _orientation_rate.ToYAMLString() +
						", \"position_variance\":" + _position_variance.ToYAMLString() +
						", \"orientation_variance\":" + _orientation_variance.ToYAMLString() +
						", \"status\":" + _status.ToYAMLString() + "}";
			}
		}
	}
}