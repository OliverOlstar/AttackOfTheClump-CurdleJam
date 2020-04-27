using UnityEngine;
using UnityEngine.Events;

public class SplineWalker : MonoBehaviour {

	[HideInInspector] public Transform target;
	public BezierSpline spline;
	[Range(0.0001f, 0.1f)] public float speedDefault;
	[Range(0.0001f, 0.1f)] public float speedDistanceMult;
	
	[Space]
	public float progress;
	public bool neverStop = false;
	public bool lookAt = true;

	private float pauseTime = 1;
	private int state = 1;

	[SerializeField] private float stopLength = 1;
	[SerializeField] private float moveLength = 1;

	public UnityEvent ReachedEnd;
	public UnityEvent Respawned;

	private void Update()
	{
		if (state == 0)
		{
			float speed = speedDefault + speedDistanceMult * Vector3.Distance(target.position, transform.position);

			progress += Time.deltaTime * speed;
			if (progress > 1f)
				progress = 1f;

			Vector3 position = spline.GetPoint(progress);
			transform.localPosition = position;

			if (lookAt)
				transform.LookAt(position + spline.GetDirection(progress));

			// Reached the end of the path
			if (progress == 1.0f)
			{
				ReachedEnd.Invoke();
				enabled = false;
			}

			if (neverStop == false && Time.time > pauseTime)
			{
				pauseTime = Time.time + stopLength;
				state = 1;
			}
		}
		else
		{
			if (neverStop || Time.time > pauseTime)
			{
				pauseTime = Time.time + moveLength;
				state = 0;
			}
		}
	}

	public void Respawn(float pProgress)
	{
		Respawned.Invoke();

		// Set Progress
		progress = pProgress;

		// Teleport back
		Vector3 position = spline.GetPoint(progress);
		transform.localPosition = position;
		transform.LookAt(position + spline.GetDirection(progress));

		// Switch To Stopped
		pauseTime = Time.time + stopLength;
		state = 1;

		enabled = true;
	}
}