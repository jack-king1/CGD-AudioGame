using UnityEngine;

public class SplineWalker : MonoBehaviour {

	public BezierSpline spline;

	public float duration;

	public bool lookForward;

    public GameObject PlayerModel;
    public Animator anim;
    public float animationSpeed = 1.0f;

	public SplineWalkerMode mode;

	private float progress;
	private bool goingForward = true;

	private void Update () {
		if (goingForward) {
			progress += Time.deltaTime / duration;
			if (progress > 1f) {
				if (mode == SplineWalkerMode.Once) {
					progress = 1f;
				}
				else if (mode == SplineWalkerMode.Loop) {
					progress -= 1f;
				}
				else {
					progress = 2f - progress;
					goingForward = false;
				}
			}
		}
		else {
			progress -= Time.deltaTime / duration;
			if (progress < 0f) {
				progress = -progress;
				goingForward = true;
			}
		}

		Vector3 position = spline.GetPoint(progress);
		transform.localPosition = position;
		if (lookForward) {
            //Get the player mage model and rotate the player but not look at point
            var lookPos = (position + spline.GetDirection(progress)) - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1);
            anim.SetBool("Moving", true);
            anim.SetFloat("InputMagnitude", animationSpeed);
            PlayerModel.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1);
        }
	}
}