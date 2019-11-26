namespace enums
{
    public enum STATE
    {
        patrol,
        chase,
        search,
        fire
    }

    public enum ENEMYTYPE
    {
        ground,
        flying,
        ranged
    }

    public enum SOUND
    {
        attack,
        chase,
        die,
        footstep
    }


    public enum CAMERASTATE
    {
        levelwon,
        levellost,
        follow,
        cinematic,
        attract
    }

    public enum PEEKDIRECTION
    {
        n,
        ne,
        e,
        se,
        s,
        sw,
        w,
        nw
    }

    public enum GAMESTATE
    {
        attract,
        game
    }

    public enum FLOORTYPE
    {
        //QUIET TO LOUD
        //Temp - Replace with actual tags
        floor,
        floor2
    }

    public enum SCENE
    {

    }
}
