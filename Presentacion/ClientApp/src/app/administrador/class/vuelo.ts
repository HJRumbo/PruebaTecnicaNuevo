import { Ciudad, Aerolinea, Estado } from '../interfaces/interfaces';
export class Vuelo {
    numeroVuelo?: number;
    ciudadOrigenId?: number;
    ciudadDestinoId?: number;
    fecha?: string;
    horaSalida?: string;
    horaLlegada?: string;
    aerolineaId?: number;
    estadoVueloId?: number;
    ciudadOrigen?: Ciudad;
    ciudadDestino?: Ciudad;
    aerolinea?: Aerolinea;
    estadoVuelo?: Estado
}