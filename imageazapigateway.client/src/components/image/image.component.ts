import { Component, OnDestroy, OnInit } from '@angular/core';
import { interval, Subscription } from 'rxjs';
import { ImageService } from 'src/services/image.service';

@Component({
  selector: 'app-image',
  templateUrl: './image.component.html',
  styleUrls: ['./image.component.css']
})
export class ImageComponent implements OnInit, OnDestroy {
  imageUrl: string = '';
  description: string = 'Loading...';
  receivedAt: string = 'Loading...';
  isLoading: boolean = true;
  totalImagesLastHour: number = 0;
  private refreshSubscription!: Subscription;

  constructor(private imageService: ImageService) {}

  ngOnInit(): void {
    this.refreshSubscription = interval(5000).subscribe(() => {
      this.reload();
    });

    this.reload();
  }

  reload() : void {
    this.loadLatestImage();
    this.loadImageStats();
  }

  loadLatestImage(): void {
    this.isLoading = true;
    this.imageService.getLatestImage().subscribe(
      (data) => {
        this.imageUrl = data.url;
        this.description = data.description;
        this.receivedAt = new Date(data.createdDate).toLocaleString();
      },
      (error) => {
        console.error('Error loading image:', error);
        this.description = 'Failed to load image.';
        this.receivedAt = '';
      },
      () => this.isLoading = false
    );
  }

  loadImageStats(): void {
    const oneHourAgoUtc = new Date(new Date().getTime() - 60 * 60 * 1000).toISOString();
    this.imageService.getTotalImagesReceived(oneHourAgoUtc).subscribe(
      (data) => {
        this.totalImagesLastHour = data.totalImages;
        this.isLoading = false;
      },
      (error) => {
        console.error('Error fetching image stats:', error);
        this.totalImagesLastHour = 0; // Fallback value
      },
      () => this.isLoading = false
    );
  }

  ngOnDestroy(): void {
    // Clean up subscriptions
    if (this.refreshSubscription) {
      this.refreshSubscription.unsubscribe();
    }
  }
}
