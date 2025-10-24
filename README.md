# TechCorp Content Hub

Enterprise CMS solution using **Contentful**, **ASP.NET Core**, and **React Widgets**

## Architecture

Hybrid architecture that mimics real-world enterprise CMS systems (Episerver/Optimizely):

- **Server-side rendering** with ASP.NET Core Razor Pages
- **Shared Sass design system** compiled via Webpack
- **React widget infrastructure** for complex interactive components
- **Single application** - no separate frontend/backend servers

## Technology Stack

**Backend:** ASP.NET Core 9.0 MVC, Contentful SDK, Serilog
**Frontend:** Sass, Webpack 5, React 19, TypeScript
**CMS:** Contentful Headless CMS

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- Node.js 18+
- Contentful account with space configured

### Setup

1. **Configure Contentful**

   Create `backend/src/TechCorp.ContentHub.API/appsettings.Development.json`:
   ```json
   {
     "Contentful": {
       "SpaceId": "your-space-id",
       "DeliveryApiKey": "your-delivery-api-key",
       "PreviewApiKey": "your-preview-api-key",
       "Environment": "master",
       "UsePreviewApi": false
     }
   }
   ```

2. **Build frontend assets**
   ```bash
   cd frontend
   npm install
   npm run build
   ```

3. **Run the application**
   ```bash
   cd ../backend/src/TechCorp.ContentHub.API
   dotnet run
   ```

4. **Visit** `http://localhost:5121`

## Development

### Frontend (Styles)
```bash
cd frontend
npm run watch  # Auto-rebuild on changes
npm run build  # Production build
```

### Backend
```bash
cd backend/src/TechCorp.ContentHub.API
dotnet watch run  # Auto-reload on changes
```

## Adding React Widgets (Phase 2)

Infrastructure is ready for React widgets on Razor pages:

1. Create widget component in `frontend/src/components/`
2. Add mounting logic in `frontend/src/index.tsx`
3. Use in Razor views:
   ```html
   <div data-react-widget="WidgetName" data-props='{"key": "value"}'></div>
   ```

## Why This Architecture?

Enterprise CMS platforms prioritize:
- **SEO** - Server-rendered HTML
- **Performance** - No large JS bundle on initial load
- **Progressive enhancement** - Add JavaScript only where needed
- **Single deployment** - Simpler infrastructure

## License

MIT
