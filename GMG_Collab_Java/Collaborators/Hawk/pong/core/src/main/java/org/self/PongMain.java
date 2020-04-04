package org.self;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input;
import com.badlogic.gdx.math.Vector2;
import org.mini2Dx.core.game.BasicGame;
import org.mini2Dx.core.geom.Circle;
import org.mini2Dx.core.geom.Rectangle;
import org.mini2Dx.core.graphics.Graphics;

public final class PongMain extends BasicGame
{
    public static final class Factory
    {
        private final int width;
        private final int height;

        public Factory(final int width,
                       final int height)
        {
            this.width = width;
            this.height = height;
        }

        // This won't work for window resizing so window size stuff
        //  would have to be mutable and these wouldn't be passed in
        public PongMain pong()
        {
            return new PongMain(width, height);
        }
    }

    @Override
    public void initialise()
    {
        // Initialize the ball moving at the player with no vertical movement
        ballVelocity.set(new Vector2(vectorX, 0));

        cpu = cpuFactory.newCpuPaddle();
    }

    @Override
    public void update(final float delta)
    {
        gameFlow();
    }

    @Override
    public void render(final Graphics g)
    {
        g.fillShape(player);
        g.fillShape(cpu.shape());
        g.fillShape(ball);
    }

    @Override
    public void interpolate(final float alpha)
    {
    }

    private void gameFlow()
    {
        handlePlayerMovement();
        ball.set(ball.getX() + ballVelocity.x, ball.getY() + ballVelocity.y);

        cpu.cpuMovement(ball);
        upperLowerBoundCollision();
        paddleCollision();
        ballScore();
    }

    private void handlePlayerMovement()
    {
        if(Gdx.input.isKeyPressed(Input.Keys.UP))
        {
            if(player.getMinY() > 0)
            {
                player.setY(player.getY() - 5f);
            }
        } else if(Gdx.input.isKeyPressed(Input.Keys.DOWN))
        {
            if(player.getMaxY() < HEIGHT)
            {
                player.setY(player.getY() + 5f);
            }
        }
    }

    private void upperLowerBoundCollision()
    {
        if(ball.getMinY() <= 0)
        {
            ballVelocity.y = -ballVelocity.y;
        }
        if(ball.getMaxY() >= HEIGHT)
        {
            ballVelocity.y = -ballVelocity.y;
        }
    }

    private void paddleCollision()
    {
        if(ball.intersects(player))
        {
            // Set Y to zero to be able to shoot up or down when colliding with
            //  upper or lower part of paddle.
            ballVelocity.y = 0;

            final float diff = Math.abs(player.getCenterY() - ball.getCenterY());
            if(ball.getCenterY() < player.getCenterY())
            {
                // TOP half of paddle
                ballVelocity.set(new Vector2(-ballVelocity.x, -Math.abs(diff / vectorY)));
            } else
            {
                ballVelocity.set(new Vector2(-ballVelocity.x, Math.abs(diff / vectorY)));
            }
        }
        if(ball.intersects(cpu.shape()))
        {
            ballVelocity.x = -ballVelocity.x;
        }
    }

    private void ballScore()
    {
        if(ball.getMinX() <= 0)
        {
            ballVelocity.x = -ballVelocity.x;
//            cpuScore++;
        }
        if(ball.getMaxX() >= WIDTH)
        {
            ballVelocity.x = -ballVelocity.x;
//            playerScore++;
        }
    }

    private PongMain(final int width,
                     final int height)
    {
        this.WIDTH = width;
        this.HEIGHT = height;
        cpuFactory = new CpuPaddle.Factory(WIDTH, HEIGHT);
        player = new Rectangle(5, HEIGHT / 2f, 10, 80);
        ball = new Circle(WIDTH / 2f, HEIGHT / 2f, 6);
    }

    private final CpuPaddle.Factory cpuFactory;

    private final Vector2 ballVelocity = new Vector2(0, 0);
    private Rectangle player;
    private Circle ball;
    private CpuPaddle cpu;

    private final int WIDTH;
    private final int HEIGHT;

//    private int playerScore = 0;
//    private int cpuScore = 0;

    private static final float vectorX = -5;
    private static final float vectorY = 3;
}
