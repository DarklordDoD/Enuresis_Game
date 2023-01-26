using UnityEngine;

public class KlikObjekt : MonoBehaviour
{
    private RaycastHit2D hit;

    /*public void OnMouseDown()
    {
        Debug.Log("kliked " + this.gameObject);
    }*/

    private void Update()
    {
        KlikObject();
    }

    private void KlikObject()
    {
        //ragistrer musseklik
        if (Input.GetMouseButtonDown(0))
            getMouseKlik(false);

        //registrer touch fra tablet
        if (Input.touchCount > 0)
            getMouseKlik(true);
    }

    private void getMouseKlik(bool isTouch)
    {
        if (isTouch)
        {
            //detect om et object bliver touchet
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.touches[0].position), Vector2.zero);
        }
        else
        {
            //detect om mousen hoveres over et object
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        }

        //Debug.DrawRay(ray.origin, ray.direction * 100, color: Color.green);

        //tester om spilleren klikker på et object
        if (hit.collider != null)
        {
            //hvis spillern klikker på pettet aktiver de pet funktionen i muvePet scriptet
            if (hit.collider.name == "Pet")
            {
                hit.collider.GetComponent<muvePet>().PetPet();              
            }

            //hvis spillern klikker på et sceneskift objekt aktiver de skiftTil fungtionen på objektet
            if (hit.collider.tag == "SceneSkift")
            {
                hit.collider.GetComponent<SceneSkift>().SkiftTil();
            }

            if (hit.collider.tag == "petSkift")
            {
                hit.collider.GetComponent<ObjecktSkift>().SkiftTil();
            }
        }
    }
}
