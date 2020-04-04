package org.self;

import org.mini2Dx.core.geom.Circle;
import org.mini2Dx.core.geom.Rectangle;

final class CpuPaddle
{
    public static final class Factory
    {
        private final int width;
        private final int height;

        public Factory(
                final int width,
                final int height)
        {
            this.width = width;
            this.height = height;
        }

        public CpuPaddle newCpuPaddle()
        {
            return new CpuPaddle(width, height);
        }
    }

    public void cpuMovement(final Circle ball)
    {
        // paddles always following Y pos of ball
        float followBall = ball.getCenterY() - rectangle.getHeight() / 2 - 7;
        rectangle.setY(followBall);
    }

    public Rectangle shape()
    {
        return rectangle;
    }

    private CpuPaddle(
            final int width,
            final int height)
    {
        rectangle = new Rectangle(width - 15, height / 2f, 10, 80);
    }

    private final Rectangle rectangle;
}
