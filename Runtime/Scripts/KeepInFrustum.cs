using UnityEngine;
namespace TGP.Utilities {
	public class KeepInFrustum : BaseMonoBehaviour {

		public Camera _camera;
		[Header("GameObject Calculations Settings")]
		public float distToCam = 3;
		public float distToFrustum = 0.5f;
		public float billboardFactor = 20f; // angle of facing player
		public bool isInFrustum = false;
		CanvasGroup[] canvasGroups;
		Canvas _canvas;
		RectTransform _rectTransform;

		protected override void Awake() {
			base.Awake();
			_rectTransform = GetComponent<RectTransform>();
			_canvas = GetComponent<Canvas>();
			if(_canvas==null )
				_canvas = GetComponentInParent<Canvas>();
			canvasGroups = GetComponentsInChildren<CanvasGroup>();
		}
		private void Start() {
			if (_camera == null) {
				_camera = Camera.main;
				Debug.LogWarning($"{TAG}No Camera set setting Camera: {_camera.name} as Cam");
			}
			isInFrustum = MenuInFrustum(_rectTransform, _camera);
			transform.position = DistToCam(_camera, distToCam);
		}
		bool MenuInFrustum(RectTransform rect, Camera camera) {

			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);

			Vector2 size = Vector2.Scale(rect.sizeDelta, rect.lossyScale);
			var v = new Vector3[4];
			_rectTransform.GetWorldCorners(v);
			var min = Vector3.positiveInfinity;
			var max = Vector3.negativeInfinity;
			// update min and max
			foreach (var vector3 in v) {
				min = Vector3.Min(min, vector3);
				max = Vector3.Max(max, vector3);
			}

			Bounds bounds = new(_canvas.transform.position, new Vector3(size.x, size.y, 1));
			bounds.SetMinMax(min, max);

			//	Debug.Log($"{TAG} center: {bounds.center} extents: {bounds.extents} max: {bounds.max} min:{bounds.min} size: {bounds.size}");
			return GeometryUtility.TestPlanesAABB(planes, bounds);
		}
		bool CheckIfNeedUpdate() {
			bool update = false;
			if (canvasGroups != null) {
				foreach (var item in canvasGroups) {
					if (item.alpha > 0) {
						update = true;
						break;
					}
				}
			}
			return update;
		}
		Quaternion Billboard(RectTransform rect, Camera cam) {
			Vector3 direction = (transform.position - cam.transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			return Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * billboardFactor);
		}
		void PushMenu(RectTransform rect, Camera camera, Quaternion rot) {
			Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);

			Vector3[] v = new Vector3[4];
			_rectTransform.GetWorldCorners(v);

			Vector3[] c = new Vector3[4];
			for (int i = 0; i < v.Length; i++) {//calculate midpoints
				int j = (i + 1) % v.Length;
				c[i] = (c[i] + c[j]) / 2;
			}
			planes = new[] { planes[0], planes[3], planes[1], planes[2] };
			float dist = 0;
			Vector3 closestP = Vector3.zero;
			Vector3 transf = Vector3.zero;
			Vector3[] pushV = new Vector3[] { Vector3.right, Vector3.down, Vector3.left, Vector3.up };
			Vector3 toCam = Vector3.zero;
			for (int i = 0; i < v.Length; i++) {
				dist = planes[i].GetDistanceToPoint(v[i]);
				if (dist <= distToFrustum) {
					transform.position += rot * (pushV[i] * (distToFrustum - dist));

					transform.position = DistToCam(camera, distToCam);
				}
			}
		}
		Vector3 DistToCam(Camera camera, float dist) {
			return camera.transform.position + ((this.transform.position - _camera.transform.position).normalized * dist);
		}
		private void Update() {
			if (CheckIfNeedUpdate() && (isInFrustum = MenuInFrustum(_rectTransform, _camera))) {
				transform.rotation = Billboard(_rectTransform, _camera);
				PushMenu(_rectTransform, _camera, transform.rotation);
			}
		}
	}
}
