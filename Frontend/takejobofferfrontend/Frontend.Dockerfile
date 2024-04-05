# Dependencies
FROM node:21-alpine3.18 as Dependencies
WORKDIR /app
COPY package.json package-lock.json* ./
RUN \
    if [ -f package-lock.json ]; then npm ci; \
    else echo "Warning: Lockfile not found. It is recommended to commit lockfiles to version control." && exit 1; \
    fi

COPY public ./public
COPY next.config.mjs .
COPY tsconfig.json .

#Build
FROM node:21-alpine3.18 as build
WORKDIR /app
COPY --from=Dependencies /app/node_modules ./node_modules
COPY . .

ENV NEXT_TELEMETRY_DISABLED 1

RUN \
    if [ -f package-lock.json ]; then SKIP_ENV_VALIDATION=1 npm run build; \
    else echo "Lockfile not found." && exit 1; \
    fi

#Runtime
FROM node:21-alpine3.18 as Runtime
WORKDIR /app

ENV NODE_ENV production
ENV NEXT_TELEMETRY_DISABLED 1

RUN addgroup --system --gid 1001 nodejs
RUN adduser --system --uid 1001 nextjs
USER nextjs

COPY --from=build /app/public ./public
COPY --from=build /app/package.json ./package.json

COPY --from=build --chown=nextjs:nodejs /app/.next/standalone ./
COPY --from=build --chown=nextjs:nodejs /app/.next/static ./.next/static

CMD ["node","server.js"]