package org.self.desktop;

import org.mini2Dx.desktop.DesktopMini2DxConfig;

import com.badlogic.gdx.backends.lwjgl.DesktopMini2DxGame;

import org.self.PongMain;

public class DesktopLauncher {
	public static void main (final String[] arg) {
		final DesktopMini2DxConfig config = new DesktopMini2DxConfig(GAME_IDENTIFIER);
		config.vSyncEnabled = false;
		config.fullscreen = false;
		config.pauseWhenBackground = true;
		config.width = PongMain.WIDTH;
		config.height = PongMain.HEIGHT;
		config.targetFPS = 30;
		new DesktopMini2DxGame(new PongMain(), config);
	}

	// Unique string identifier for the game or something
	private static final String GAME_IDENTIFIER = " org.self";
}
