using UnityEngine;

public class DistanceControl : MonoBehaviour {

    private GameObject topPoint;
    private GameObject fitPoint;
    private GameObject cutPoint;
    private GameObject spawnPoint;
    private GameObject mainCamera;
    private GameObject dLight;
    private GameObject background;
    private GameObject particleSys;
    private GameObject panelParticle;

    private Vector3 distanceBetweenFitAndTop;
    private Vector3 distanceBetweenCutAndTop;
    private Vector3 distanceBetweenSpawnAndTop;
    private Vector3 distanceBetweenCamAndTop;
    private Vector3 distanceBetweenLightAndTop;
    private Vector3 distanceBetweenBGAndTop;
    private Vector3 distanceBetweenParticleSysAndTop;
    private Vector3 distanceBetweenPanelParticleAndTop;

    private void Start ()
    {
        findObjects();
        calculateDistance();
	}

    private void findObjects()
    {
        topPoint       = GameObject.FindGameObjectWithTag("topPointTag");
        fitPoint       = GameObject.FindGameObjectWithTag("fitPointTag");
        cutPoint       = GameObject.FindGameObjectWithTag("cutPointTag");
        spawnPoint     = GameObject.FindGameObjectWithTag("spawnPointTag");
        mainCamera     = GameObject.FindGameObjectWithTag("MainCamera");
        dLight         = GameObject.FindGameObjectWithTag("lightTag");
        background     = GameObject.FindGameObjectWithTag("arkaplanTag");
        particleSys    = GameObject.FindGameObjectWithTag("particleSystemTag");
        panelParticle  = GameObject.FindGameObjectWithTag("panelParticleTag");
    }

    private void calculateDistance()
    {
        distanceBetweenFitAndTop            = fitPoint.transform.position      - topPoint.transform.position;
        distanceBetweenCutAndTop            = cutPoint.transform.position      - topPoint.transform.position;
        distanceBetweenSpawnAndTop          = spawnPoint.transform.position    - topPoint.transform.position;
        distanceBetweenCamAndTop            = mainCamera.transform.position    - topPoint.transform.position;
        distanceBetweenLightAndTop          = dLight.transform.position        - topPoint.transform.position;
        distanceBetweenBGAndTop             = background.transform.position    - topPoint.transform.position;
        distanceBetweenParticleSysAndTop    = particleSys.transform.position   - topPoint.transform.position;
        distanceBetweenPanelParticleAndTop  = panelParticle.transform.position - topPoint.transform.position;
    }

    public void setDistance()
    {
        fitPoint.transform.position       = topPoint.transform.position + distanceBetweenFitAndTop;
        cutPoint.transform.position       = topPoint.transform.position + distanceBetweenCutAndTop;
        spawnPoint.transform.position     = topPoint.transform.position + distanceBetweenSpawnAndTop;
        mainCamera.transform.position     = topPoint.transform.position + distanceBetweenCamAndTop;
        dLight.transform.position         = topPoint.transform.position + distanceBetweenLightAndTop;
        background.transform.position     = topPoint.transform.position + distanceBetweenBGAndTop;
        particleSys.transform.position    = topPoint.transform.position + distanceBetweenParticleSysAndTop;
        panelParticle.transform.position  = topPoint.transform.position + distanceBetweenPanelParticleAndTop;
    }
}
