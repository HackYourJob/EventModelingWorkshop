<?php


namespace App\Controller;


use App\ReadModels\RoomTypes;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Response;

class RoomTypesDashboardController extends AbstractController
{
    /**
     * @var RoomTypes
     */
    private $roomTypes;

    public function __construct(RoomTypes $roomTypes)
    {
        $this->roomTypes = $roomTypes;
    }

    public function __invoke(): Response
    {
        return $this->render('room_types_dashboard.twig', [
            'types' => ($this->roomTypes)(),
        ]);
    }
}
