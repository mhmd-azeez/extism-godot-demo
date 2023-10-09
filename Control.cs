using Extism.Sdk;
using Extism.Sdk.Native;

using Godot;
using System;
using System.IO;
using System.Text;

public partial class Control : Godot.Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	private void _on_button_pressed()
	{
		var textEdit = GetNode<TextEdit>("InputText");
		var label = GetNode<Label>("ResultLabel");

		label.Text = textEdit.Text;

		var bytes = File.ReadAllBytes("count_vowels.wasm");
		using var plugin = Plugin.Create(bytes, Array.Empty<HostFunction>(), withWasi: true);
		label.Text = Encoding.UTF8.GetString(
			plugin.CallFunction("count_vowels", 
				Encoding.UTF8.GetBytes(textEdit.Text)));
	}

}
