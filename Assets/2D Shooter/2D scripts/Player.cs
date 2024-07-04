using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Interactions;

namespace shooter
{
    public class Player : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] RectTransform playerRectTransform;
        [SerializeField] Canvas canvas;
        [SerializeField] RectTransform canvasRectTransform;
        [SerializeField] float minXclamp;
        [SerializeField] float maxXclamp;
        [SerializeField] float minYclamp;
        [SerializeField] float maxYclamp;
        [SerializeField] Transform[] bullets;


        InputControls _inputControls;


        private void Awake()
        {
            _inputControls = new InputControls();
        }

        private void OnEnable()
        {
            _inputControls.Enable();
        }

        private void OnDisable()
        {
            _inputControls.Disable();
        }

        void Start()
        {
            minXclamp = -(canvasRectTransform.rect.width - playerRectTransform.sizeDelta.x) / 2;
            maxXclamp =  (canvasRectTransform.rect.width - playerRectTransform.sizeDelta.x) / 2;
            
            minYclamp = (0 + playerRectTransform.sizeDelta.y / 2);
            maxYclamp = (canvasRectTransform.rect.height * 0.25f - playerRectTransform.sizeDelta.y/2);

            
        }

        private void Update()
        {
            _inputControls.Player.Fire.started += ctx =>
            {
                
                if(ctx.interaction is PressInteraction)
                {
                    Invoke(nameof(ShootBullet), 0);
                    
                }
            };
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            var xClamp = Mathf.Clamp(playerRectTransform.anchoredPosition3D.x, minXclamp, maxXclamp);
            var yClamp = Mathf.Clamp(playerRectTransform.anchoredPosition3D.y, minYclamp, maxYclamp);
            playerRectTransform.anchoredPosition = new Vector2 (xClamp, yClamp);
            playerRectTransform.anchoredPosition += new Vector2(eventData.delta.x, eventData.delta.y) / canvas.scaleFactor;
            
        }

        Vector3 GetMouseWorldPos()
        {
            return Vector3.zero;
        }

        void ShootBullet()
        {
            foreach(Transform bullet in bullets)
            {
                if (!bullet.gameObject.activeInHierarchy)
                {
                    bullet.gameObject.SetActive(true);
                    break;
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            CancelInvoke(nameof(ShootBullet));
        }
    }
}
