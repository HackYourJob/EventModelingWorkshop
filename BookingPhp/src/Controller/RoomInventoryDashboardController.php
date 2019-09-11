<?php

namespace App\Controller;

use App\ReadModels\RoomInventory;
use App\ReadModels\RoomTypes;
use Symfony\Bundle\FrameworkBundle\Controller\AbstractController;
use Symfony\Component\HttpFoundation\Response;

class RoomInventoryDashboardController extends AbstractController
{
    /**
     * @var RoomInventory
     */
    private $roomInventory;

    /**
     * @var RoomTypes
     */
    private $roomTypes;

    public function __construct(RoomInventory $roomInventory, RoomTypes $roomTypes)
    {
        $this->roomInventory = $roomInventory;
        $this->roomTypes = $roomTypes;
    }

    public function __invoke(): Response
    {
        return $this->render('room_inventory_dashboard.twig', [
            'rooms' => ($this->roomInventory)(),
            'types' => array_column(($this->roomTypes)(), 'type'),
        ]);
    }
}
