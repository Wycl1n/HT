import { ReviewInfoModel } from "./review-info.model";

export interface BookInfoModel {
    bookId: number;
    title: string;
    content: string;
    cover: string;
    author: string;
    genre: string;
    rating: number;
    reviews: ReviewInfoModel[];
}