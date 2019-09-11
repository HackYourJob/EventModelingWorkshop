<?php

namespace App\Controller;

use Symfony\Component\HttpFoundation\Response;

class HelloController
{
    public function __invoke()
    {
        return new Response(
            '<html><body>Hello</body></html>'
        );
    }
}
