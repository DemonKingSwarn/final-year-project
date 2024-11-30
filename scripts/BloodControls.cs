using Godot;

public partial class BloodControls : CpuParticles2D
{
	
	void OnBloodTimeout()
	{
		SetProcess(false);
		SetPhysicsProcess(false);
		SetProcessInput(false);
		SetProcessInternal(false);
		SetProcessUnhandledInput(false);
		SetProcessUnhandledKeyInput(false);
	}
}
