package org.self;

import org.newdawn.slick.AppGameContainer;
import org.newdawn.slick.BasicGame;
import org.newdawn.slick.GameContainer;
import org.newdawn.slick.Graphics;
import org.newdawn.slick.Input;
import org.newdawn.slick.SlickException;
import org.newdawn.slick.geom.Circle;
import org.newdawn.slick.geom.Rectangle;
import org.newdawn.slick.geom.RoundedRectangle;
import org.newdawn.slick.geom.Vector2f;

public final class Main extends BasicGame {
    public static void main(final String[] args) {
        try {
            final AppGameContainer gameContainer =
                    new AppGameContainer(new Main("Simple Slick MAIN is a weird name"));
            gameContainer.setDisplayMode(WIDTH, HEIGHT, false);
//            gameContainer.setShowFPS(false);
            gameContainer.setTargetFrameRate(30);
            gameContainer.start();
        } catch (final SlickException e) {
            System.err.println("" + e);
        }
    }

    public Main(final String gameName) {
        super(gameName);
    }

    @Override
    public void init(final GameContainer gameContainer) {
        gameContainer.getInput().enableKeyRepeat();
        player = new RoundedRectangle(5, HEIGHT / 2f, 10, 80, 3);
        cpu = new RoundedRectangle(WIDTH - 15, HEIGHT / 2f, 10, 80, 3);
        ball = new Circle((float) WIDTH / 2, (float) HEIGHT / 2, 6);
        ballVelocity = new Vector2f(vectorX, vectorY);
    }

    @Override
    public void update(final GameContainer gameContainer, final int i) {
        if (gameContainer.getInput().isKeyDown(Input.KEY_ESCAPE)) {
            gameContainer.exit();
        }
        // This kinda works BUT because it's happening very fast the input
        //  is recorded multiple times before you the key press releases
        if (gameContainer.getInput().isKeyDown(Input.KEY_SPACE)) {
            if (gameContainer.isPaused()) {
                gameContainer.setPaused(false);
            } else {
                gameContainer.setPaused(true);
            }
        }
        gameFlow(gameContainer);
    }

    @Override
    public void render(final GameContainer gameContainer, final Graphics graphics) {
        graphics.fill(player);
        graphics.fill(cpu);
        graphics.fill(ball);
    }

    private void gameFlow(final GameContainer gameContainer) {
        if (!gameContainer.isPaused()) {
            handlePlayerMovement(gameContainer);
            ball.setLocation(ball.getX() + ballVelocity.getX(), ball.getY() + ballVelocity.getY());

            handleBallVert();
            handlePaddleCollision();
            handleBallScore();

            // paddles always following Y pos of ball
            float followBall = ball.getCenterY() - cpu.getHeight() / 2;
            cpu.setY(followBall);
//        player.setY(followBall);
        }
    }

    private void handlePlayerMovement(final GameContainer gameContainer) {
        if (gameContainer.getInput().isKeyDown(Input.KEY_UP)) {
            if (player.getMinY() > 0) {
                player.setY(player.getY() - 10f);
            }
        } else if (gameContainer.getInput().isKeyDown(Input.KEY_DOWN)) {
            if (player.getMaxY() < HEIGHT) {
                player.setY(player.getY() + 10f);
            }
        }
    }

    // TODO(shf): Rename
    private void handleBallVert() {
        if (ball.getMinY() <= 0) {
            ballVelocity.y = -ballVelocity.getY();
        }
        if (ball.getMaxY() >= HEIGHT) {
            ballVelocity.y = -ballVelocity.getY();
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
                ballVelocity = new Vector2f(-ballVelocity.getX(), -Math.abs(diff / vectorY));
            } else {
                ballVelocity = new Vector2f(-ballVelocity.getX(), Math.abs(diff / vectorY));
            }
        }
        if (ball.intersects(cpu)) {
            ballVelocity.x = -ballVelocity.getX();
        }
    }

    private void handleBallScore() {
        if (ball.getMinX() <= 0) {
            ballVelocity.x = -ballVelocity.getX();
            cpuScore++;
        }
        if (ball.getMaxX() >= WIDTH) {
            ballVelocity.x = -ballVelocity.getX();
            playerScore++;
        }
    }

    private Circle ball;
    private Rectangle player;
    private Rectangle cpu;
    private Vector2f ballVelocity;
    private static final float vectorX = -10;
    private static final float vectorY = 2;

    private int playerScore;
    private int cpuScore;

    private static final int WIDTH = 640;
    private static final int HEIGHT = 480;
    private static final String MESSAGE = "WELCOME TO THE WORLD";
}
