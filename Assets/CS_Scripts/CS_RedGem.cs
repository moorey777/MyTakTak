using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CS_RedGem : MonoBehaviour
{
    public GameObject gem;
    protected float my_Dis;
    protected float my_Height;
    public Vector3 cubeDirection;

    protected GameObject newobj;
    public GameObject barHigh, barLow;
    public Image abilityImage1;
    public float cooldown1 = 40;
    bool isCooldown1 = false;

    public Image abilityImage2;
    public float cooldown2 = 40;
    bool isCooldown2 = false;

    public Material colorRed, colorYellow, colorGreen;
    private Color red, yellow, green;


    void Start()
    {
        red = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        yellow = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        green = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
    }

    void Update()
    {
        if (isCooldown1)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;
            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                isCooldown1 = false;
            }
        }
        if (isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;
            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        other.gameObject.SetActive(false);
        if (other.GetComponent<Renderer>().material.color == red) {
            Debug.Log("Red gem acquired! Now you have hint bars.");
            StopCoroutine("Cooldown1");
            abilityImage1.fillAmount = 0;
            isCooldown1 = false;
            StartCoroutine("Cooldown1");
        } else if (other.GetComponent<Renderer>().material.color == yellow) {
            Debug.Log("Get a coin!");
            addCoin();
        } else if (other.GetComponent<Renderer>().material.color == green) {
            Debug.Log("Green gem acquired! Now you only have normal cubes!");
            abilityImage2.fillAmount = 0;
            isCooldown2 = false;
            StopCoroutine("Cooldown2");
            StartCoroutine("Cooldown2");
        }
    }

    public void ManualTriggerRed() {
        int gemR = PlayerPrefs.GetInt("gemR");
        Debug.Log(gemR);
        if (gemR > 0) {
            PlayerPrefs.SetInt("gemR", gemR-1);
            StopCoroutine("Cooldown1");
            abilityImage1.fillAmount = 0;
            Debug.Log(abilityImage1.fillAmount);
            isCooldown1 = false;
            StartCoroutine("Cooldown1");
        }
    }

    public void ManualTriggerGreen() {
        int gemG = PlayerPrefs.GetInt("gemG");
        if (gemG > 0) {
            PlayerPrefs.SetInt("gemG", gemG-1);
            StopCoroutine("Cooldown2");
            abilityImage2.fillAmount = 0;
            isCooldown2 = false;
            StartCoroutine("Cooldown2");
        }
    }

    public GameObject newBox(GameObject NowCube, float dis, Vector3 cubeDirPassed) {

        newobj = GameObject.Instantiate (gem) as GameObject;
        int gemChoice = Random.Range(0, 6);
        if (gemChoice > 0 && gemChoice < 4) {
            newobj.GetComponent<Renderer>().material = colorRed;
        } else if (gemChoice == 0) {
            newobj.GetComponent<Renderer>().material = colorGreen;
        } else {
            newobj.GetComponent<Renderer>().material = colorYellow;
        }

        my_Dis = dis / 2;
        my_Height = 10f;

        cubeDirection = cubeDirPassed;
        Vector3 pos = Vector3.zero;
        if (NowCube == null)
        {
            pos = cubeDirection * my_Dis + transform.position;
        }
        else {
            pos = cubeDirection * my_Dis + NowCube.transform.position;
        }

        pos.y = my_Height;
        newobj.transform.position = pos;


        if (cubeDirection.z != 0)
        {
            Vector3 rotationVector = transform.rotation.eulerAngles;
            rotationVector.y = 0;
            newobj.transform.rotation = Quaternion.Euler(rotationVector);
        }

        return newobj;
    }

    public void addCoin() {
        int coins = PlayerPrefs.GetInt("coins");
        PlayerPrefs.SetInt("coins", coins+1);
    }

    IEnumerator Cooldown1() {
            isCooldown1 = true;
            abilityImage1.fillAmount = 1;
            barHigh.SetActive(true);
            barLow.SetActive(true);
            yield return new WaitForSeconds(40f);
            barHigh.SetActive(false);
            barLow.SetActive(false);
            // isCooldown1 = false;
    }

    IEnumerator Cooldown2() {
            isCooldown2 = true;
            abilityImage2.fillAmount = 1;
            PlayerPrefs.SetInt("easy", 1);
            yield return new WaitForSeconds(40f);
            PlayerPrefs.SetInt("easy", 0);
            // isCooldown2 = false;
    }
}
