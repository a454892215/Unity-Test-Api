using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityStandardAssets.CrossPlatformInput;

namespace Standard_Assets.CrossPlatformInput.Scripts
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public enum AxisOption
        {
            // Options for which axes to use
            Both, // Use both
            OnlyHorizontal, // Only horizontal
            OnlyVertical // Only vertical
        }

        [FormerlySerializedAs("MovementRange")]
        public int movementRange = 100;

        public AxisOption axesToUse = AxisOption.Both; // The options for the axes that the still will use

        public string
            horizontalAxisName = "Horizontal"; // The name given to the horizontal axis for the cross platform input

        public string verticalAxisName = "Vertical"; // The name given to the vertical axis for the cross platform input

        Vector3 _mStartPos;
        bool _mUseX; // Toggle for using the x axis
        bool _mUseY; // Toggle for using the Y axis

        CrossPlatformInputManager.VirtualAxis
            _mHorizontalVirtualAxis; // Reference to the joystick in the cross platform input

        CrossPlatformInputManager.VirtualAxis
            _mVerticalVirtualAxis; // Reference to the joystick in the cross platform input

        private float _posX;

        void OnEnable()
        {
            CreateVirtualAxes();
        }

        void Start()
        {
            var transform1 = transform;
            _mStartPos = transform1.position;
        }

        //更新虚拟轴
        void UpdateVirtualAxes(Vector3 value)
        {
            var delta = _mStartPos - value;
            delta.y = -delta.y;
            delta /= movementRange;
            if (_mUseX)
            {
                _mHorizontalVirtualAxis.Update(-delta.x);
                print("=========UpdateVirtualAxes========x :" + (-delta.x));
            }

            if (_mUseY)
            {
                _mVerticalVirtualAxis.Update(delta.y);
            }
        }

        //创建虚拟轴
        void CreateVirtualAxes()
        {
            // set axes to use
            _mUseX = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyHorizontal);
            _mUseY = (axesToUse == AxisOption.Both || axesToUse == AxisOption.OnlyVertical);

            // create new axes based on axes to use
            if (_mUseX)
            {
                _mHorizontalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(horizontalAxisName);
                CrossPlatformInputManager.RegisterVirtualAxis(_mHorizontalVirtualAxis);
            }

            if (_mUseY)
            {
                _mVerticalVirtualAxis = new CrossPlatformInputManager.VirtualAxis(verticalAxisName);
                CrossPlatformInputManager.RegisterVirtualAxis(_mVerticalVirtualAxis);
            }
        }


        //当拖动
        public void OnDrag(PointerEventData data)
        {
            Vector3 newPos = Vector3.zero;

            if (_mUseX)
            {
                int delta = (int) (data.position.x - _mStartPos.x);
                delta = Mathf.Clamp(delta, -movementRange, movementRange);
                newPos.x = delta;
            }

            if (_mUseY)
            {
                int delta = (int) (data.position.y - _mStartPos.y);
                delta = Mathf.Clamp(delta, -movementRange, movementRange);
                newPos.y = delta;
            }

            var transform1 = transform;
            transform1.position =
                new Vector3(_mStartPos.x + newPos.x, _mStartPos.y + newPos.y, _mStartPos.z + newPos.z);
            UpdateVirtualAxes(transform1.position);
        }


        //当手指抬起
        public void OnPointerUp(PointerEventData data)
        {
            transform.position = _mStartPos;
            UpdateVirtualAxes(_mStartPos);
        }


        //当手指按下
        public void OnPointerDown(PointerEventData data)
        {
            int x = data.position.x > _mStartPos.x ? 1 : -1;
            _mHorizontalVirtualAxis.Update(x);
        }

        void OnDisable()
        {
            // remove the joysticks from the cross platform input
            if (_mUseX)
            {
                _mHorizontalVirtualAxis.Remove();
            }

            if (_mUseY)
            {
                _mVerticalVirtualAxis.Remove();
            }
        }
    }
}