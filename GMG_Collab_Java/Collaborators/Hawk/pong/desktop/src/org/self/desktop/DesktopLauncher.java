package org.self.desktop;

import org.mini2Dx.desktop.DesktopMini2DxConfig;

import com.badlogic.gdx.backends.lwjgl.DesktopMini2DxGame;

import org.self.PongMain;

public class DesktopLauncher
{
    public static void main(final String[] arg)
    {
        final DesktopMini2DxConfig config = new DesktopMini2DxConfig(GAME_IDENTIFIER);
        config.vSyncEnabled = false;
        config.fullscreen = false;
        config.pauseWhenBackground = true;
        config.width = WIDTH;
        config.height = HEIGHT;
        config.targetFPS = 30;
        new DesktopMini2DxGame(
                new PongMain.Factory(WIDTH, HEIGHT).pong(),
                config);
    }

    // Unique string identifier for the game or something
    private static final String GAME_IDENTIFIER = " org.self";
    private static final int WIDTH = 900;
    private static final int HEIGHT = 480;
}
