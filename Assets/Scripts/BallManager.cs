using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BallManager : SingletonManager<BallManager>
{
    [SerializeField] private Transform[] ballPoints;
    [SerializeField] private List<GameObject> ballGroups;
    [SerializeField] private List<GameObject> collectedGroups = new List<GameObject>();
    [SerializeField] private List<GameObject> collectedBalls = new List<GameObject>();

    private bool hasFinished;
    private int collectedAmount;

    private int totalAmount;
    public int TotalAmount
    {
        get { return totalAmount; }
        private set { totalAmount = value; }
    }

    private void Awake()
    {
        GameObject[] ballGroupObjects = GameObject.FindGameObjectsWithTag("BallGroup");

        foreach (GameObject ballGroupObject in ballGroupObjects)
        {
            ballGroups.Add(ballGroupObject);
        }

        totalAmount = TotalBallAmount();
    }
    void Start()
    {
        collectedAmount = 0;
    }
    public IEnumerator CheckIfBallsFree()
    {
        yield return new WaitForSeconds(.15f);

        for (int i = ballGroups.Count - 1; i >= 0; i--)
        {
            RaycastHit hit;
            if (Physics.Raycast(ballGroups[i].transform.position, Vector3.up, out hit, Mathf.Infinity))
            {
                if (hit.collider != null && hit.collider.CompareTag("RayControl"))
                {
                    GameObject _group = ballGroups[i];

                    ballGroups.Remove(_group);
                    collectedGroups.Add(_group.gameObject);

                    GameManager.Instance.IsGridMoving = true;

                    for (int j = 0; j < _group.transform.childCount; j++)
                    {
                        Transform _ball = _group.transform.GetChild(j).transform;

                        int ran = Random.Range(0, ballPoints.Length);
                        float ranPower = Random.Range(2f, 2.25f);
                        int ranJump = Random.Range(1, 3);
                        float ranDur = 0.4f;

                        _ball.DOJump(ballPoints[ran].position, ranPower, ranJump, ranDur).OnComplete(() =>
                        {
                            _ball.gameObject.AddComponent<Rigidbody>();
                            collectedAmount++;
                            UIManager.Instance.UpdateCollectedAmount(collectedAmount);
                        });

                        yield return new WaitForSeconds(0.1f);

                        if (!collectedBalls.Contains(_ball.gameObject))
                        {
                            collectedBalls.Add(_ball.gameObject);
                        }
                    }

                    GameManager.Instance.IsGridMoving = false;
                }
            }
        }

        if (ballGroups.Count > 0)
            yield break;

        StartCoroutine(LineUpBalls());
    }

    private IEnumerator LineUpBalls()
    {
        yield return new WaitForSeconds(3f);

        GameManager.Instance.MoveAwayGridals();

        yield return new WaitForSeconds(0.5f);

        RewardBox.Instance.ShowRewardBox();
        AnimationManager.Instance.BallBoxWin();

        yield return new WaitForSeconds(1.5f);

        if (!hasFinished)
        {
            hasFinished = true;

            for (int i = 0; i < collectedBalls.Count; i++)
            {
                collectedBalls[i].transform.SetParent(RewardBox.Instance.transform);

                collectedBalls[i].GetComponent<Rigidbody>().isKinematic = true;

                Vector3 _pos = RewardBox.Instance.BallSlots[i].position;
                float _power = 2f;
                int _jumpCount = 1;
                float _dur = .5f;

                collectedBalls[i].transform.DOJump(_pos, _power, _jumpCount, _dur);

                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(1.1f);

            AnimationManager.Instance.RewardBoxTurning();
            UIManager.Instance.WinPanelSwitch();
        }
    }

    private int TotalBallAmount()
    {
        int _total = ballGroups.Count * 9;
        return _total;
    }
}
