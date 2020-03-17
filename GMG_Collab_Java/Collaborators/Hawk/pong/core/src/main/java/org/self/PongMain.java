package org.self;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input;
import com.badlogic.gdx.math.Vector2;
import org.mini2Dx.core.game.BasicGame;
import org.mini2Dx.core.geom.Circle;
import org.mini2Dx.core.geom.Rectangle;
import org.mini2Dx.core.graphics.Graphics;

public class PongMain extends BasicGame {
    @Override
    public void initialise() {
        player = new Rectangle(5, HEIGHT / 2f, 10, 80);
        cpu = new Rectangle(WIDTH - 15, HEIGHT / 2f, 10, 80);
        ball = new Circle(WIDTH / 2f, HEIGHT / 2f, 6);
        ballVelocity = new Vector2(vectorX, vectorY);

        ball = new Circle(WIDTH / 2f, HEIGHT / 2f, 6);
    }

    @Override
    public void update(final float delta) {
        gameFlow();
    }

    @Override
    public void render(final Graphics g) {
        player.fill(g);
        cpu.fill(g);
        ball.fill(g);
    }

    @Override
    public void interpolate(final float alpha) {
    }

    private void gameFlow() {
        handlePlayerMovement();
        ball.set(ball.getX() + ballVelocity.x, ball.getY() + ballVelocity.y);

        handleUpperAndLowerBoundCollision();
        handlePaddleCollision();
        handleBallScore();

        // paddles always following Y pos of ball
        float followBall = ball.getCenterY() - cpu.getHeight() / 2 - 7;
        cpu.setY(followBall);
//        player.setY(followBall);
    }

    private void handlePlayerMovement() {
        if (Gdx.input.isKeyPressed(Input.Keys.UP)) {
            if (player.getMinY() > 0) {
                player.setY(player.getY() - 10f);
            }
        } else if (Gdx.input.isKeyPressed(Input.Keys.DOWN)) {
            if (player.getMaxY() < HEIGHT) {
                player.setY(player.getY() + 10f);
            }
        }
    }

    private void handleUpperAndLowerBoundCollision() {
        if (ball.getMinY() <= 0) {
            ballVelocity.y = -ballVelocity.y;
        }
        if (ball.getMaxY() >= HEIGHT) {
            ballVelocity.y = -ballVelocity.y;
        }
    }

    private void handlePaddleCollision() {
        // Using centerY, minY, && maxY, we could do similar logic as unity game
        //  for collision and changing Vector direction. maxY() is bottom of paddle, minY() is top
        //  centerY is middle of paddle. They change based on the position of the paddle in the window (0-480/489)
        // because window border adds 9 that the paddle goes past right now.

        // TO GET DIFFERENCE take paddle.getcenterY - ball.getcenterY and return absolute value
        if (ball.intersects(player)) {
            // Set Y to zero to be able to shoot up or down when colliding with
            //  upper or lower part of paddle.
            ballVelocity.y = 0;

            final float diff = Math.abs(player.getCenterY() - ball.getCenterY());
            if (ball.getCenterY() < player.getCenterY()) {
                // TOP half of paddle
                ballVelocity = new Vector2(-ballVelocity.x, -Math.abs(diff / vectorY));
            } else {
                ballVelocity = new Vector2(-ballVelocity.x, Math.abs(diff / vectorY));
            }
        }
        if (ball.intersects(cpu)) {
            ballVelocity.x = -ballVelocity.x;
        }
    }

    private void handleBallScore() {
        if (ball.getMinX() <= 0) {
            ballVelocity.x = -ballVelocity.x;
            cpuScore++;
        }
        if (ball.getMaxX() >= WIDTH) {
            ballVelocity.x = -ballVelocity.x;
            playerScore++;
        }
    }

    private Circle ball;
    private Rectangle player;
    private Rectangle cpu;
    private Vector2 ballVelocity;
    private static final float vectorX = -5;
    private static final float vectorY = 3;

    private int playerScore;
    private int cpuScore;

    public static final int WIDTH = 900;
    public static final int HEIGHT = 480;
}
