#pragma strict
#pragma downcast

var hitPoints = 100.0;
var deathEffect : Transform;
var effectDelay = 0.0;
private var gos : GameObject[];
var multiplier : float = 1;
var deadReplacement : Rigidbody;
@HideInInspector
var playerObject : GameObject;
var useHitEffect : boolean = true;

@HideInInspector
var isEnemy : boolean = false;
private var animator : Animator;
//var particleSys : ParticleSystem;
private var enemyAudio : AudioSource;
private var enemyCapCollider : CapsuleCollider;
private var isSinking : boolean;
private var isDead : boolean;

public var sinkSpeed : float;
public var scoreValue : float;
public var deathClip : AudioClip;

function Start() {
	animator = GetComponent(Animator);
	//particleSys = GetComponentInChildren(ParticleSystem);
	enemyAudio = GetComponent(AudioSource);
	enemyCapCollider = GetComponent(CapsuleCollider);
	isSinking = false;
	isDead = false;
}

function Update() {
	// If the enemy should be sinking...
    if(isSinking)
    {
        // ... move the enemy down by the sinkSpeed per second.
        transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
    }

}

function ApplyDamage(Arr : Object[]){
	//Info array contains damage and value of fromPlayer boolean (true if the player caused the damage)
	//Find the player if we haven't
	if(Arr[1] == true){
		if(!playerObject){
			playerObject = GameObject.FindWithTag("Player");
		}
		if(useHitEffect){
			playerObject.BroadcastMessage("HitEffect");
		}
	}
	
	// We already have less than 0 hitpoints, maybe we got killed already?
	if (hitPoints <= 0.0)
		return;
	var tempFloat : float;
	tempFloat = Arr[0];
	//float.TryParse(Arr[0], tempFloat);
	hitPoints -= tempFloat*multiplier;
	enemyAudio.Play ();
	Debug.Log("hit!,points:" + hitPoints);
	
	if (hitPoints <= 0.0) {
		// Start emitting particles
		var emitter : ParticleEmitter = GetComponentInChildren(ParticleEmitter);
		if (emitter)
			emitter.emit = true;
		Death();
		//Invoke("DelayedDetonate", effectDelay);
		
	}
}

function ApplyDamagePlayer (damage : float){
	//Info array contains damage and value of fromPlayer boolean (true if the player caused the damage)
	//Find the player if we haven't
	if(!playerObject){
		playerObject = GameObject.FindWithTag("Player");
	}
	if(useHitEffect){
		playerObject.BroadcastMessage("HitEffect");
	}
	
	// We already have less than 0 hitpoints, maybe we got killed already?
	if (hitPoints <= 0.0)
		return;
	//float.TryParse(Arr[0], tempFloat);
	hitPoints -= damage*multiplier;
	enemyAudio.Play ();
	Debug.Log("hit2!,points:" + hitPoints);
	
	if (hitPoints <= 0.0) {
		// Start emitting particles
		var emitter : ParticleEmitter = GetComponentInChildren(ParticleEmitter);
		if (emitter)
			emitter.emit = true;
		Death();
		//Invoke("DelayedDetonate", effectDelay);
	}
}

function ApplyDamage (damage : float){
	//Info array contains damage and value of fromPlayer boolean (true if the player caused the damage)
	//Find the player if we haven't
	
	// We already have less than 0 hitpoints, maybe we got killed already?
	if (hitPoints <= 0.0)
		return;

	//float.TryParse(Arr[0], tempFloat);
	hitPoints -= damage*multiplier;
	enemyAudio.Play ();
	Debug.Log("hit3!,points:" + hitPoints);
	
	if (hitPoints <= 0.0) {
		// Start emitting particles
		var emitter : ParticleEmitter = GetComponentInChildren(ParticleEmitter);
		if (emitter)
			emitter.emit = true;
		Death();
		//Invoke("DelayedDetonate", effectDelay);
	}
}

function DelayedDetonate(){
	BroadcastMessage ("Detonate");
}

function Detonate(){
	if(isEnemy)
		EnemyMovement.enemies--;
	// Create the deathEffect
	if (deathEffect)
		Instantiate (deathEffect, transform.position, transform.rotation);

	// If we have a dead replacement then replace ourselves with it!
	if (deadReplacement) {
		var dead : Rigidbody = Instantiate(deadReplacement, transform.position, transform.rotation);

		// For better effect we assign the same velocity to the exploded gameObject
		dead.rigidbody.velocity = rigidbody.velocity;
		dead.angularVelocity = rigidbody.angularVelocity;
	}
	
	// If there is a particle emitter stop emitting and detach so it doesnt get destroyed right away
	var emitter : ParticleEmitter = GetComponentInChildren(ParticleEmitter);
	if (emitter){
		emitter.emit = false;
		emitter.transform.parent = null;
	}
	BroadcastMessage ("Die", SendMessageOptions.DontRequireReceiver);
	Destroy(gameObject);

}

function Death ()
{
    // The enemy is dead.
    isDead = true;

    // Turn the collider into a trigger so shots can pass through it.
    enemyCapCollider.isTrigger = true;

    // Tell the animator that the enemy is dead.
    animator.SetTrigger ("Dead");

    // Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
    enemyAudio.clip = deathClip;
    enemyAudio.Play ();
}


function StartSinking ()
{
    // Find and disable the Nav Mesh Agent.
    GetComponent(NavMeshAgent).enabled = false;

    // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
    GetComponent(Rigidbody).isKinematic = true;

    // The enemy should no sink.
    isSinking = true;

    // Increase the score by the enemy's score value.
    //ScoreManager.score += scoreValue;

    // After 2 seconds destory the enemy.
    Destroy (gameObject, 2f);
}
